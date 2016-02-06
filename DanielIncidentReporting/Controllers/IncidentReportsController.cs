using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DanielIncidentReporting.Models;
using System.Data.Entity.Validation;

namespace DanielIncidentReporting.Controllers
{
    public class IncidentReportsController : Controller
    {
        private IRTSDBContext2 db = new IRTSDBContext2();
        // GET: IncidentReports
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            ApplicationUser user = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            if (user != null)
            {
                if (user.mgrPosition.Equals("Residential Manager"))
                {
                    return View(db.IncidentReports.Where(m => m.IRP_ProgramName.Equals(user.Program) && m.IRP_ApprovalLevelReq.Equals("0")));
                }
                else if (user.mgrPosition.Equals("Department Director"))
                {
                    return View(db.IncidentReports.Where(m => m.IRP_ProgramName.Equals(user.Program) && m.IRP_ApprovalLevelReq.Equals("1")));
                }
                else if (user.mgrPosition.Equals("Risk Manager"))
                {
                    return View(db.IncidentReports.Where(m => m.IRP_ApprovalLevelReq.Equals("2")));
                }
               
            }
            return View();
        }
        //GET: IncidentReports
        public ActionResult Approve(int? id)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            ApplicationUser user = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            IncidentReport incidentReport = db.IncidentReports.Find(id);

            if (user.mgrPosition.Equals("Residential Manager"))
            {
                incidentReport.IRP_ApprovalLevelReq = "1";
                incidentReport.IRP_ResMgrApprovedDate = System.DateTime.Now;
            }
            else if (user.mgrPosition.Equals("Department Director"))
            {
                incidentReport.IRP_ApprovalLevelReq = "2";
                incidentReport.IRP_DeptDirApprovedDate = System.DateTime.Now;
            }
            else if (user.mgrPosition.Equals("Risk Manager"))
            {
                incidentReport.IRP_ApprovalLevelReq = "3";
                incidentReport.IRP_RiskMgrApprovedDate = System.DateTime.Now;
            }

            db.IncidentReports.AddOrUpdate(incidentReport);
            db.SaveChanges();
            return View();
        }

        // GET: IncidentReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentReport incidentReport = db.IncidentReports.Find(id);
            if (incidentReport == null)
            {
                return HttpNotFound();
            }
            return View(incidentReport);
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        // GET: IncidentReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidentReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IncidentReport incidentReport, Incident incident)
        {
            if (ModelState.IsValid)
            {
                if (incidentReport.IRP_ProgramName.Equals("SIPP"))
                {
                    incidentReport.IRP_ApprovalLevelReq = "0";
                }
                else
                {
                    incidentReport.IRP_ApprovalLevelReq = "1";
                }

                db.IncidentReports.Add(incidentReport);
                db.SaveChanges();
                incident.IRP_ID = incidentReport.IRP_ID;
                incident.INT_ID = 1;
                db.Incidents.Add(incident);
                db.SaveChanges();
                return RedirectToAction("Confirmation");  
            }

            return View();
        }

        // GET: IncidentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentReport incidentReport = db.IncidentReports.Find(id);
            if (incidentReport == null)
            {
                return HttpNotFound();
            }
            return View(incidentReport);
        }

        // POST: IncidentReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IRP_ID, IRP_Category, IRP_Location, IRP_ReportDate, IRP_IncidentDate, IRP_VictimFirstName, IRP_VictimLastName, IRP_ReportOn, IRP_ResMgrApprovedDate, IRP_DeptDirApprovedDate, IRP_RiskMgrApprovedDate, IRP_RiskMgrComment, IRP_PreparedByFirstName, IRP_PreparedByLastName, IRP_Description, IRP_InjuryType, IRP_BodyPart, IRP_InjuryFollowUp, IRP_ApprovalLevelReq, IRP_ProgramName")] IncidentReport incidentReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidentReport);
        }

        // GET: IncidentReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentReport incidentReport = db.IncidentReports.Find(id);
            if (incidentReport == null)
            {
                return HttpNotFound();
            }
            return View(incidentReport);
        }

        // POST: IncidentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentReport incidentReport = db.IncidentReports.Find(id);
            db.IncidentReports.Remove(incidentReport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var program in db.Programs)
            {
                if (program.Prg_Active.Equals("1"))
                {
                    list.Add(new SelectListItem() { Value = program.Prg_Name, Text = program.Prg_Name });
                }
            }

            SelectList programs = new SelectList(list, "Value", "Text");
            ViewBag.programs = programs;

            //InjuryFollowUpList for dropdown
            List<SelectListItem> injuryFollowUpList = new List<SelectListItem>();
            foreach (var injuryFollowUp in db.InjuryFollowUps)
            {
                injuryFollowUpList.Add(new SelectListItem() { Value = injuryFollowUp.IFU_name, Text = injuryFollowUp.IFU_name });
            }
            //is this repeating what the above code is doing? - Gina
            SelectList injuryFollowUps = new SelectList(injuryFollowUpList, "Value", "Text"); 
            ViewBag.injuryFollowUps = injuryFollowUps;

            // Contributing Factors - 1. Abuse allegation dropdown list items - Gina Chin Fatt
            List<SelectListItem> abuseAllegationItems = new List<SelectListItem>();
            abuseAllegationItems.Add(new SelectListItem { Value = "-1", Text = "Abuse/sexual encounter", Selected = true, Disabled = true });
            abuseAllegationItems.Add(new SelectListItem {Value="Client/Client", Text="Client/Client"});
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Staff", Text = "Client/Staff" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Parent", Text = "Client/Parent" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Other", Text = "Client/Other" });

            ViewBag.abuseAllegationItems = abuseAllegationItems;

            // Contributing Factors - 2. Physical aggression dropdown list items - Gina Chin Fatt
            List<SelectListItem> physicalAggressionItems = new List<SelectListItem>();
            physicalAggressionItems.Add(new SelectListItem { Value = "-1", Text = "Physical aggression", Selected = true, Disabled = true });
            physicalAggressionItems.Add(new SelectListItem { Value = "Toward others", Text = "Toward others" });
            physicalAggressionItems.Add(new SelectListItem { Value = "Toward self", Text = "Toward self" });
 
            ViewBag.physicalAggressionItems = physicalAggressionItems;

            // Contributing Factors - 3. Physical aggression dropdown list items - Gina Chin Fatt
            List<SelectListItem> policeInvolvementItems = new List<SelectListItem>();
            policeInvolvementItems.Add(new SelectListItem { Value = "-1", Text = "Involvement with police/fire/rescue", Selected = true, Disabled = true });
            policeInvolvementItems.Add(new SelectListItem { Value = "Baker act", Text = "Baker act" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Medical emergency", Text = "Medical emergency" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Elopement", Text = "Elopement" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Criminal activity", Text = "Criminal activity" });
            policeInvolvementItems.Add(new SelectListItem { Value = "False alarm", Text = "False alarm" });
            
            ViewBag.policeInvolvementItems = policeInvolvementItems;

            // Contributing Factors - 3. Cause of Injury dropdown list items - Gina Chin Fatt
            List<SelectListItem> injuryItems = new List<SelectListItem>();
            injuryItems.Add(new SelectListItem { Value = "-1", Text = "Cause of injury", Selected = true, Disabled = true});
            injuryItems.Add(new SelectListItem { Value = "Baker act", Text = "Baker act" });
            injuryItems.Add(new SelectListItem { Value = "Medical emergency", Text = "Medical emergency" });
            injuryItems.Add(new SelectListItem { Value = "Elopement", Text = "Elopement" });
            injuryItems.Add(new SelectListItem { Value = "Criminal activity", Text = "Criminal activity" });
            injuryItems.Add(new SelectListItem { Value = "False alarm", Text = "False alarm" });

            ViewBag.injuryItems = injuryItems;

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
                if (incidentReport.IRP_ProgramName.Equals("SIPP - Statewide In-patient Psychiatric Program"))
                {
                    incidentReport.IRP_ApprovalLevelReq = "0";
                }
                else
                    incidentReport.IRP_ApprovalLevelReq = "1";

                db.IncidentReports.Add(incidentReport);
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
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var program in db.Programs)
            {
                if (program.Prg_Active.Equals("1"))
                {
                    list.Add(new SelectListItem() { Value = program.Prg_Name, Text = program.Prg_Name });
                }
            }

            SelectList programs = new SelectList(list, "Value", "Text");
            ViewBag.programs = programs;


            //InjuryFollowUpList for dropdown
            List<SelectListItem> injuryFollowUpList = new List<SelectListItem>();
            foreach (var injuryFollowUp in db.InjuryFollowUps)
            {
                injuryFollowUpList.Add(new SelectListItem() { Value = injuryFollowUp.IFU_name, Text = injuryFollowUp.IFU_name });
            }

            SelectList injuryFollowUps = new SelectList(injuryFollowUpList, "Value", "Text");
            ViewBag.injuryFollowUps = injuryFollowUps;

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
            return View();
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

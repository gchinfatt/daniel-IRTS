using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DanielIncidentReporting.Models;

namespace DanielIncidentReporting.Controllers
{
    public class IncidentReportsController : Controller
    {
        private IRTSDBContext db = new IRTSDBContext();

        // GET: IncidentReports
        public ActionResult Index()
        {
            return View(db.IncidentReports.ToList());
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
        public ActionResult Create([Bind(Include = "id,incidentDate,programName,location,incidentType,reporterFirstName,reporterLastName,Description")] IncidentReport incidentReport)
        {
            if (ModelState.IsValid)
            {
                db.IncidentReports.Add(incidentReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incidentReport);
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
        public ActionResult Edit([Bind(Include = "id,incidentDate,programName,location,incidentType,reporterFirstName,reporterLastName,Description")] IncidentReport incidentReport)
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

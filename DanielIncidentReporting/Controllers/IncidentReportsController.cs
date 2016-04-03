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
using System.Text;
using System.Web.Services.Protocols;

namespace DanielIncidentReporting.Controllers
{
    public class IncidentReportsController : Controller
    {
        private IRTSDBContext2 db = new IRTSDBContext2();

        public void populateViewDataItems(IncidentReport incidentReport, Incident incident, int? id)
        {
            //ViewBag.programs
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "", Text = "Select Program", Selected = true, Disabled = true });
            foreach (var program in db.Programs)
            {
                if (program.Prg_Active.Equals("1"))
                {
                    list.Add(new SelectListItem() { Value = program.Prg_Name, Text = program.Prg_Name });
                }
            }
            SelectList programs = new SelectList(list, "Value", "Text");

            ViewBag.programs = programs;

            // Contributing Factors - 1. Abuse Allegation dropdown list items - Gina Chin Fatt
            List<SelectListItem> abuseAllegationItems = new List<SelectListItem>();
            abuseAllegationItems.Add(new SelectListItem { Value = "-1", Text = "Abuse Allegation", Selected = true, Disabled = true });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Client", Text = "Client/Client" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Staff", Text = "Client/Staff" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Parent", Text = "Client/Parent" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Other", Text = "Client/Other" });

            ViewBag.abuseAllegationItems = abuseAllegationItems;

            // Contributing Factors - 2. Physical Aggression dropdown list items - Gina Chin Fatt
            List<SelectListItem> physicalAggressionItems = new List<SelectListItem>();
            physicalAggressionItems.Add(new SelectListItem { Value = "-1", Text = "Physical Restraint", Selected = true, Disabled = true });
            physicalAggressionItems.Add(new SelectListItem { Value = "Physical Agression Toward Others", Text = "Physical Agression Toward Others" });
            physicalAggressionItems.Add(new SelectListItem { Value = "Physical Agression Toward Self", Text = "Physical Agression Toward Self" });

            ViewBag.physicalAggressionItems = physicalAggressionItems;

            // Contributing Factors - 2a. Physical Restraint Times
            if (incidentReport != null)
                {
                String IRP_RestraintSTTimeValue = incidentReport.IRP_RestraintSTTime;
                String IRP_RestraintENTimeValue = incidentReport.IRP_RestraintENTime;

                ViewBag.IRP_RestraintSTTimeValue = IRP_RestraintSTTimeValue;
                ViewBag.IRP_RestraintENTimeValue = IRP_RestraintENTimeValue;
            }
            else if (incidentReport == null)
            {
                ViewBag.IRP_RestraintSTTimeValue = "";
                ViewBag.IRP_RestraintENTimeValue = "";
            }

            // Contributing Factors - 3. Police Involvement dropdown list items - Gina Chin Fatt
            List<SelectListItem> policeInvolvementItems = new List<SelectListItem>();
            policeInvolvementItems.Add(new SelectListItem { Value = "-1", Text = "Police/Fire/Rescue", Selected = true, Disabled = true });
            policeInvolvementItems.Add(new SelectListItem { Value = "Baker Act", Text = "Baker act" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Medical Emergency", Text = "Medical Emergency" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Elopement", Text = "Elopement" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Criminal Activity", Text = "Criminal Activity" });
            policeInvolvementItems.Add(new SelectListItem { Value = "False Alarm", Text = "False Alarm" });

            ViewBag.policeInvolvementItems = policeInvolvementItems;

            // Contributing Factors - 3a. Police Report Number
            if (incidentReport != null)
            {
                String IRP_PoliceRepNoValue = incidentReport.IRP_PoliceRepNo;

                ViewBag.IRP_PoliceRepNoValue = IRP_PoliceRepNoValue;
            }
            else if (incidentReport == null)
            {
                ViewBag.IRP_PoliceRepNoValue = "";
            }

            // Contributing Factors - 4. Injury Items dropdown list items - Gina Chin Fatt
            List<SelectListItem> injuryItems = new List<SelectListItem>();
            injuryItems.Add(new SelectListItem { Value = "-1", Text = "Cause of Injury", Selected = true, Disabled = true });
            injuryItems.Add(new SelectListItem { Value = "Slip/Fall", Text = "Slip/Fall" });
            injuryItems.Add(new SelectListItem { Value = "Bite", Text = "Bite" });
            injuryItems.Add(new SelectListItem { Value = "During ESI", Text = "During ESI" });
            injuryItems.Add(new SelectListItem { Value = "Self-Inflicted", Text = "Self-Inflicted" });
            injuryItems.Add(new SelectListItem { Value = "Other", Text = "Other" });

            ViewBag.injuryItems = injuryItems;

            // Contributing Factors - 4a. Injury FollowUp dropdown list items
            List<SelectListItem> injuryFollowUpItems = new List<SelectListItem>();
            injuryFollowUpItems.Add(new SelectListItem { Value = "-1", Text = "Injury Follow-Up", Selected = true, Disabled = true });
            injuryFollowUpItems.Add(new SelectListItem { Value = "First Aid", Text = "First Aid" });
            injuryFollowUpItems.Add(new SelectListItem { Value = "ER", Text = "ER" });
            injuryFollowUpItems.Add(new SelectListItem { Value = "On-Site Consult", Text = "On-Site Consult" });
            injuryFollowUpItems.Add(new SelectListItem { Value = "Non-emergency Doctor's Visit", Text = "Non-emergency Doctor's Visit" });
            injuryFollowUpItems.Add(new SelectListItem { Value = "None Needed", Text = "None Needed" });

            ViewBag.injuryFollowUpItems = injuryFollowUpItems;

            // Contributing Factors - 5. Unplanned Hospitalization dropdown list items
            List<SelectListItem> unplannedHospitalizationItems = new List<SelectListItem>();
            unplannedHospitalizationItems.Add(new SelectListItem { Value = "-1", Text = "Unplanned Hospitalization", Selected = true, Disabled = true });
            unplannedHospitalizationItems.Add(new SelectListItem { Value = "Baker Act", Text = "Baker act" });
            unplannedHospitalizationItems.Add(new SelectListItem { Value = "Medical Emergency", Text = "Medical Emergency" });
            unplannedHospitalizationItems.Add(new SelectListItem { Value = "Elopement", Text = "Elopement" });
            unplannedHospitalizationItems.Add(new SelectListItem { Value = "Criminal Activity", Text = "Criminal Activity" });
            unplannedHospitalizationItems.Add(new SelectListItem { Value = "False Alarm", Text = "False Alarm" });

            ViewBag.unplannedHospitalizationItems = unplannedHospitalizationItems;

            // Contributing Factors - 6. Sexual Encounter dropdown list items
            List<SelectListItem> sexualEncounterItems = new List<SelectListItem>();
            sexualEncounterItems.Add(new SelectListItem { Value = "-1", Text = "Sexual Encounter", Selected = true, Disabled = true });
            sexualEncounterItems.Add(new SelectListItem { Value = "Client/Client", Text = "Client/Client" });
            sexualEncounterItems.Add(new SelectListItem { Value = "Client/Staff", Text = "Client/Staff" });
            sexualEncounterItems.Add(new SelectListItem { Value = "Client/Parent", Text = "Client/Parent" });
            sexualEncounterItems.Add(new SelectListItem { Value = "Client/Other", Text = "Client/Other" });

            ViewBag.sexualEncounterItems = sexualEncounterItems;

            // Contributing Factors - 7. Seclusion dropdown list items
            List<SelectListItem> seclusionItems = new List<SelectListItem>();
            seclusionItems.Add(new SelectListItem { Value = "-1", Text = "Seclusion", Selected = true, Disabled = true });
            seclusionItems.Add(new SelectListItem { Value = "Physical Agression Toward Others", Text = "Physical Agression Toward Others" });
            seclusionItems.Add(new SelectListItem { Value = "TPhysical Agression Toward Self", Text = "Physical Agression Toward Self" });

            ViewBag.seclusionItems = seclusionItems;

            // Contributing Factors - 7a. Seclusion Times
            if (incidentReport != null)
            {
                String IRP_SeclusionSTTimeValue = incidentReport.IRP_SeclusionSTTime;
                String IRP_SeclusionENTimeValue = incidentReport.IRP_SeclusionENTime;

                ViewBag.IRP_SeclusionSTTimeValue = IRP_SeclusionSTTimeValue;
                ViewBag.IRP_SeclusionENTimeValue = IRP_SeclusionENTimeValue;
            }
            else if (incidentReport == null)
            {
                ViewBag.IRP_SeclusionSTTimeValue = "";
                ViewBag.IRP_SeclusionENTimeValue = "";
            }
        }
        

        // Get: IncidentReports
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            
            ApplicationUser user = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            if (user != null)
            {
                if (user.mgrPosition.Equals("Residential Manager"))
                {
                    return
                        View(
                            db.IncidentReports.Where(
                                m => m.IRP_ProgramName.Equals(user.Program) && m.IRP_ApprovalLevelReq.Equals("0")));
                }
                else if (user.mgrPosition.Equals("Department Director"))
                {
                    return
                        View(
                            db.IncidentReports.Where(
                                m => m.IRP_ProgramName.Equals(user.Program) && (m.IRP_ApprovalLevelReq.Equals("0") || m.IRP_ApprovalLevelReq.Equals("1"))));
                }
                else if (user.mgrPosition.Equals("Risk Manager"))
                {
                    ViewBag.position = "RiskManager";
                    return
                        View(
                            db.IncidentReports.Where(
                                m =>
                                    m.IRP_ApprovalLevelReq.Equals("0") || m.IRP_ApprovalLevelReq.Equals("1") ||
                                    m.IRP_ApprovalLevelReq.Equals("2")));
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
                incidentReport.IRP_ResMgrEmail = user.Email;
            }
            else if (user.mgrPosition.Equals("Department Director"))
            {
                incidentReport.IRP_ApprovalLevelReq = "2";
                incidentReport.IRP_DeptDirApprovedDate = System.DateTime.Now;
                incidentReport.IRP_DepDirEmail = user.Email;
            }
            else if (user.mgrPosition.Equals("Risk Manager"))
            {
                incidentReport.IRP_ApprovalLevelReq = "3";
                incidentReport.IRP_RiskMgrApprovedDate = System.DateTime.Now;
                incidentReport.IRP_RiskMgrEmail = user.Email;
            }

            db.IncidentReports.AddOrUpdate(incidentReport);
            db.SaveChanges();
            return View();
        }

        //GET ExportCSV
        public ActionResult ExportCSV()
        {

            return View();
        }

        //POST ExportCSV
        [HttpPost]
        public ActionResult ExportCSV(ExportReport report)
        {
            List<IncidentReport> incidents = new List<IncidentReport>();
            int lastRow = 1;

            //Filter incidents by date
            foreach (var incident in db.IncidentReports)
            {
                if (incident.IRP_IncidentDate >= report.fromDate && incident.IRP_IncidentDate <= report.toDate)
                {
                    incidents.Add(incident);
                    lastRow++;
                }
            }

            //Put filtered incidents into a string builder
            StringBuilder sb = new StringBuilder();

            //Column headers
            string columnHeaders = "Id" + "," + "Program" + ", " + "Category" + ", " + "Location" + "," + "Report Date" +
                                   "," + "Incident Date" + "," + "Victim First Name" +
                                   "," + "Victim Last Name" + ", " + "Report On" + "," + "Res. Mgr. Email" + "," +
                                   "Res. Mgr. Appr. Date" + "," + "Dep. Dir. Email" + "," + "Dep. Dir. Appr. Date" +
                                   "," + "Risk Mgr. Email" + "," + "Risk Mgr. Appr. Date" + "," + "Risk Mgr. Comment" +
                                   "," + "Reporter First Name" + "," + "Reporter Last Name" +
                                   "," + "Description" + "," + "Witnesses" + "," + "Notifications" + "," +
                                   "Abuse Allegation" + "," + "Death" + "," + "Police/Fire" + "," + "Suicide Gestures" +
                                   "," + "Unpl. Hospitalization" + "," + "AMA" + "," + "Sexual Encounter" + "," +
                                   "Substance Abuse" + "," + "Med. Error" + "," + "Injury" + "," + "Client Grievance" +
                                   "," + "Phys. Restraint" + "," + "Seclusion" + "," + "Prop. Damage" + "," +
                                   "Prop. Missing" + "," + "Theft" + "," + "Other" + "," + "Police Report Nbr." +
                                   "," + "Restraint Start Time" + "," + "Restraint End Time" + "," +
                                   "Seclusion Start Time" + "," + "Seclusion End Time" + "," + "CF: Abuse Allegation" +
                                   "," + "CF: Phys. Restraint" + "," + "CF: Police Involvement" + "," + "CF: Injury" +
                                   "," + "CF: Injury Other Description" + "," + "CF: Unpl. Hosp." + "," +
                                   "CF: Sexual Encounter" + "," + "CF: Seclusion" +
                                   "," + "Injury Type" + "," + "Injury Body Part" + "," + "Injury Follow Up";


            
            sb.AppendLine(columnHeaders);

            //Actual values of each filtered incident
            foreach (var incident in incidents)
            {
                string columnValues = incident.IRP_ID + "," + incident.IRP_ProgramName + ", " + incident.IRP_Category + ", " +
                                  EscapeCSV(incident.IRP_Location) + "," + formatDate(incident.IRP_ReportDate) + "," +
                                  formatDate(incident.IRP_IncidentDate) + "," + EscapeCSV(incident.IRP_VictimFirstName) +
                                  "," + EscapeCSV(incident.IRP_VictimLastName) + "," + EscapeCSV(incident.IRP_ReportOn) + "," + EscapeCSV(incident.IRP_ResMgrEmail) +
                                  "," + formatDate(incident.IRP_ResMgrApprovedDate) + "," + EscapeCSV(incident.IRP_DepDirEmail) + "," +
                                  formatDate(incident.IRP_DeptDirApprovedDate) + "," + EscapeCSV(incident.IRP_RiskMgrEmail) + "," +
                                  formatDate(incident.IRP_RiskMgrApprovedDate) +
                                  "," + EscapeCSV(incident.IRP_RiskMgrComment) + "," + EscapeCSV(incident.IRP_PreparedByFirstName) + "," +
                                  EscapeCSV(incident.IRP_PreparedByLastName) + "," + EscapeCSV(incident.IRP_Description) + "," +
                                  EscapeCSV(incident.IRP_Witness) + "," + EscapeCSV(incident.IRP_Notified) +
                                  "," + incident.IRP_AbuseAllegation + "," + incident.IRP_Death + "," +
                                  incident.IRP_PoliceFire + "," + incident.IRP_SuicideGestures + "," +
                                  incident.IRP_UnplannedHospitalization + "," + incident.IRP_AMA + "," +
                                  incident.IRP_SexualEncounter +
                                  "," + incident.IRP_SubstanceAbuse + "," + incident.IRP_MedicationError + "," +
                                  incident.IRP_Injury + "," + incident.IRP_ClientGrievance + "," +
                                  incident.IRP_PhysicalRestraint + "," + incident.IRP_Seclusion + "," +
                                  incident.IRP_PropertyDamage +
                                  "," + incident.IRP_PropertyMissing + "," + incident.IRP_Theft + "," +
                                  incident.IRP_Other + "," + EscapeCSV(incident.IRP_PoliceRepNo) + "," +
                                  EscapeCSV(incident.IRP_RestraintSTTime) + "," + EscapeCSV(incident.IRP_RestraintENTime) + "," +
                                  EscapeCSV(incident.IRP_SeclusionSTTime) +
                                  "," + EscapeCSV(incident.IRP_SeclusionENTime) + "," + incident.IRP_ContribAbuseAllegation + "," +
                                  incident.IRP_ContribPhysicalAggression + "," + incident.IRP_ContribPoliceInvolvement +
                                  "," + incident.IRP_ContribInjuryItems + "," + incident.IRP_ContribInjuryOtherText +
                                  "," + incident.IRP_ContribUnplannedHospitalization +
                                  "," + incident.IRP_ContribSexualEncounter + "," + incident.IRP_ContribSeclusion + "," +
                                  EscapeCSV(incident.IRP_InjuryType) + "," + EscapeCSV(incident.IRP_BodyPart) + "," +
                                  EscapeCSV(incident.IRP_InjuryFollowUp);

                sb.AppendLine(columnValues); ;
            }

            string totalHeaders = ",,,,,,,,,,,,,,,,,,,,," + "Abuse Allegation" +
                                  "," + "Death" + "," + "Police/Fire" + "," + "Suicide Gestures" +
                                  "," + "Unpl. Hospitalization" + "," + "AMA" + "," + "Sexual Encounter" + "," +
                                  "Substance Abuse" + "," + "Med. Error" + "," + "Injury" + "," + "Client Grievance" +
                                  "," + "Phys. Restraint" + "," + "Seclusion" + "," + "Prop. Damage" + "," +
                                  "Prop. Missing" +
                                  "," + "Theft" + "," + "Other";

            string totalValues = ",,,,,,,,,,,,,,,,,,,," + "Total" + "," + "=SUM(V2: V" + lastRow + ")" + "," +
                                 "=SUM(W2: w" + lastRow + ")" + "," + "=SUM(X2: X" + lastRow + ")" + "," + "=SUM(Y2: Y" +
                                 lastRow + ")" + "," + "=SUM(Z2: Z" + lastRow + ")" +
                                 "," + "=SUM(AA2: AA" + lastRow + ")" + "," + "=SUM(AB2: AB" + lastRow + ")" + "," +
                                 "=SUM(AC2: AC" + lastRow + ")" + "," + "=SUM(AD2: AD" + lastRow + ")" + "," +
                                 "=SUM(AE2: AE" + lastRow + ")" + "," + "=SUM(AF2: AF" + lastRow + ")" +
                                 "," + "=SUM(AG2: AG" + lastRow + ")" + "," + "=SUM(AH2: AH" + lastRow + ")" + "," +
                                 "=SUM(AI2: AI" + lastRow + ")" + "," + "=SUM(AJ2: AJ" + lastRow + ")" + "," +
                                 "=SUM(AK2: AK" + lastRow + ")" + "," + "=SUM(AL2: AL" + lastRow + ")";
            sb.AppendLine();
            sb.AppendLine(totalHeaders);
            sb.AppendLine(totalValues);
            

            String csv = sb.ToString();
            String reportName = "Incidents" + report.fromDate.ToShortDateString() + "_" + report.toDate.ToShortDateString() + ".csv";

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", reportName);
        }

        public string formatDate(DateTime date)
        {
            string formattedDate = date.ToShortDateString();

            if (formattedDate == "1/1/0001")
            {
                formattedDate = null;
            }
            return formattedDate;
        }
        public string EscapeCSV(string toEscape)
        {
            if (toEscape != null)
            {
                if (toEscape.Contains("\""))
                {
                    toEscape = toEscape.Replace("\"", "\"\"");
                }

                if (toEscape.Contains(","))
                {
                    toEscape = string.Format("\"{0}\"", toEscape);
                }

                if (toEscape.Contains(System.Environment.NewLine))
                {
                    toEscape = string.Format("\"{0}\"", toEscape);
                }
            }
           
            return toEscape;
        }
        //GET search by name page
        public ActionResult SearchByName()
        {
            return View();
        }
        //POST search by name page
        [HttpPost]
        public ActionResult SearchByName(String lastName)
        {
            return View("searchByNameResult", db.IncidentReports.Where(m => m.IRP_VictimLastName.Equals(lastName)));
        }

        public ActionResult SearchDetails(int? id)
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

            populateViewDataItems(null, null, id);

            //ViewBag.mgrPosition
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser userPosition = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            String mgrPosition = userPosition.mgrPosition;

            ViewBag.mgrPosition = mgrPosition;

            return View(incidentReport);
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

            populateViewDataItems(null, null, id);

            //ViewBag.mgrPosition
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser userPosition = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            String mgrPosition = userPosition.mgrPosition;

            ViewBag.mgrPosition = mgrPosition;

            return View(incidentReport);
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        // GET: IncidentReports/Create
        public ActionResult Create()
        {
            populateViewDataItems(null, null, null);

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
                try
                {
                    //If the incident program is SIPP
                    if (incidentReport.IRP_ProgramName.Equals("SIPP - Statewide In-patient Psychiatric Program"))
                    {
                        incidentReport.IRP_ApprovalLevelReq = "0";
                    }
                    else
                        incidentReport.IRP_ApprovalLevelReq = "1";

                    //Nature of incident
                    if (incidentReport.IRP_AbuseAllegation == "1" || incidentReport.IRP_Death == "1" ||
                        incidentReport.IRP_PoliceFire == "1" || incidentReport.IRP_SuicideGestures == "1" ||
                        incidentReport.IRP_UnplannedHospitalization == "1")
                    {
                        incidentReport.IRP_Category = "Serious";
                    }
                    else
                    {
                        incidentReport.IRP_Category = "Regular";
                    }

                    db.IncidentReports.Add(incidentReport);
                    db.SaveChanges();
                    return RedirectToAction("Confirmation");
                }
                catch
                {
                    populateViewDataItems(incidentReport, incident, null);

                    return View();
                }
            }
            else
            {
                populateViewDataItems(incidentReport, incident, null);

                return View();
            }
        }

        // GET: IncidentReports/Edit/5
        public ActionResult Edit(int? id)
        {
            IncidentReport incidentReport = db.IncidentReports.Find(id);
            IncidentReport incidentReportData = db.IncidentReports.Find(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (incidentReport == null)
            {
                return HttpNotFound();
            }

            populateViewDataItems(null, null, id);

            //ViewBag.mgrPosition
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser userPosition = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();

            String mgrPosition = userPosition.mgrPosition;

            ViewBag.mgrPosition = mgrPosition;

            return View(incidentReport);
        }

        // POST: IncidentReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include =
            "IRP_ID, " +
            "IRP_Category, " +
            "IRP_Location, " +
            "IRP_ReportDate, " +
            "IRP_IncidentDate, " +
            "IRP_IncidentTime, " +
            "IRP_VictimFirstName, " +
            "IRP_VictimLastName, " +
            "IRP_ReportOn, " +
            "IRP_ResMgrApprovedDate, " +
            "IRP_DeptDirApprovedDate, " +
            "IRP_RiskMgrApprovedDate, " +
            "IRP_RiskMgrComment, " +
            "IRP_PreparedByFirstName, " +
            "IRP_PreparedByLastName, " +
            "IRP_Description, " +
            "IRP_InjuryType, " +
            "IRP_BodyPart, " +
            "IRP_InjuryFollowUp, " +
            "IRP_ApprovalLevelReq, " +
            "IRP_ProgramName, " +
            "IRP_Witness, " +
            "IRP_Notified, " +
            "IRP_ContribAbuseAllegation, " +
            "IRP_ContribPhysicalAggression, " +
            "IRP_ContribPoliceInvolvement, " +
            "IRP_ContribInjuryItems, " +
            "IRP_ContribInjuryOtherText, " +
            "IRP_ContribUnplannedHospitalization, " +
            "IRP_ContribSexualEncounter, " +
            "IRP_ContribSeclusion, " +
            "IRP_AbuseAllegation, " +
            "IRP_AMA, " +
            "IRP_Death, " +
            "IRP_PoliceFire, " +
            "IRP_SuicideGestures, " +
            "IRP_UnplannedHospitalization, " +
            "IRP_SexualEncounter, " +
            "IRP_SubstanceAbuse, " +
            "IRP_MedicationError, " +
            "IRP_Injury, " +
            "IRP_ClientGrievance, " +
            "IRP_PhysicalRestraint, " +
            "IRP_Seclusion, " +
            "IRP_PropertyDamage, " +
            "IRP_PropertyMissing, " +
            "IRP_Theft, " +
            "IRP_Other, " +
            "IRP_RestraintSTTime, " +
            "IRP_RestraintENTime, " +
            "IRP_SeclusionSTTime, " +
            "IRP_SeclusionENTime, " +
            "IRP_PoliceRepNo"
            )] IncidentReport incidentReport)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //Nature of incident
                    if (incidentReport.IRP_AbuseAllegation == "1" || incidentReport.IRP_Death == "1" ||
                        incidentReport.IRP_PoliceFire == "1" || incidentReport.IRP_SuicideGestures == "1" ||
                        incidentReport.IRP_UnplannedHospitalization == "1")
                    {
                        incidentReport.IRP_Category = "Serious";
                    }
                    else
                    {
                        incidentReport.IRP_Category = "Regular";
                    }

                    db.Entry(incidentReport).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    populateViewDataItems(incidentReport, null, null);
                    
                    //ViewBag.mgrPosition
                    ApplicationDbContext context = new ApplicationDbContext();
                    ApplicationUser userPosition = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();

                    String mgrPosition = userPosition.mgrPosition;

                    ViewBag.mgrPosition = mgrPosition;

                    return View(incidentReport);
                }
                
            }
            else
            {
                populateViewDataItems(incidentReport, null, null);

                //ViewBag.mgrPosition
                ApplicationDbContext context = new ApplicationDbContext();
                ApplicationUser userPosition = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();

                String mgrPosition = userPosition.mgrPosition;

                ViewBag.mgrPosition = mgrPosition;

                return View(incidentReport);
            }
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
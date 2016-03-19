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

namespace DanielIncidentReporting.Controllers
{
    public class IncidentReportsController : Controller
    {
        private IRTSDBContext2_TJA_test db = new IRTSDBContext2_TJA_test();

        // GET: IncidentReports
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
                                m => m.IRP_ProgramName.Equals(user.Program) && m.IRP_ApprovalLevelReq.Equals("1")));
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

        //GET ExportCSV
        public ActionResult ExportCSV() {
            
            return View();
        }

        //POST ExportCSV
        [HttpPost]
        public ActionResult ExportCSV(DateTime dateFrom, DateTime dateTo)
        {
            List<IncidentReport> incidents = new List<IncidentReport>();

            //Filter incidents by date
            foreach (var incident in db.IncidentReports)
            {
                if (incident.IRP_IncidentDate >= dateFrom && incident.IRP_IncidentDate <= dateTo)
                {
                    incidents.Add(incident);
                }
            }

            //Put filtered incidents into a string builder
            StringBuilder sb = new StringBuilder();
            
            //Column headers
            sb.AppendLine("Id" + "," + "Program" + ", " + "Category" + ", " + "Location" + "," + "Report Date" + "," + "Incident Date" + "," + "Victim First Name" +
                    "," + "Victim Last Name" + ", " + "Report On" + "," + "Res. Mgr. Email" + "," + "Res. Mgr. Appr. Date" + "," + "Dep. Dir. Email" + "," + "Dep. Dir. Appr. Date" + 
                    "," + "Risk Mgr. Email" + "," + "Risk Mgr. Appr. Date" + "," + "Risk Mgr. Comment" + "," + "Reporter First Name" + "," + "Reporter Last Name" + 
                    "," + "Description" + "," + "Witnesses" + "," + "Notifications" + "," + "Abuse Allegation" + "," + "Death" + "," + "Police/Fire" + "," + "Suicide Gestures" + 
                    "," + "Unpl. Hospitalization" + "," + "AMA" + "," + "Sexual Encounter" + "," + "Substance Abuse" + "," + "Med. Error" + "," + "Injury" + "," + "Client Grievance" + 
                    "," + "Phys. Restraint" + "," + "Seclusion" + "," + "Prop. Damage" + "," + "Prop. Missing" + "," + "Theft" + "," + "Other" + "," + "Police Report Nbr." + 
                    "," + "Restraint Start Time" + "," + "Restraint End Time" + "," + "Seclusion Start Time" + "," + "Seclusion End Time" + "," + "CF: Abuse Allegation" + 
                    "," + "CF: Phys. Restraint" + "," + "CF: Police Involvement" + "," + "CF: Injury" + "," + "CF: Unpl. Hosp." + "," + "CF: Sexual Encounter" + "," + "CF: Seclusion" + 
                    "," + "Injury Type" + "," + "Injury Body Part" + "," + "Injury Follow Up");

            //Actual values of each filtered incident
            foreach (var incident in incidents)
            {
                sb.AppendLine(incident.IRP_ID + "," + incident.IRP_ProgramName + ", " + incident.IRP_Category + ", " + incident.IRP_Location + "," + incident.IRP_ReportDate + "," + incident.IRP_IncidentDate + "," + incident.IRP_VictimFirstName +
                    "," + incident.IRP_VictimLastName + "," + incident.IRP_ReportOn + "," + "Res. Email" + "," + incident.IRP_ResMgrApprovedDate + "," + "Dep. Dir. Email" + "," + incident.IRP_DeptDirApprovedDate + "," + "Risk Email" + "," + incident.IRP_RiskMgrApprovedDate +
                    "," + incident.IRP_RiskMgrComment + "," + incident.IRP_PreparedByFirstName + "," + incident.IRP_PreparedByLastName + "," + incident.IRP_Description + "," + incident.IRP_Witness + "," + incident.IRP_Notified +
                    "," + incident.IRP_AbuseAllegation + "," + incident.IRP_Death + "," + incident.IRP_PoliceFire + "," + incident.IRP_SuicideGestures + "," + incident.IRP_UnplannedHospitalization + "," + "AMA" + "," + incident.IRP_SexualEncounter +
                    "," + incident.IRP_SubstanceAbuse + "," + incident.IRP_MedicationError + "," + incident.IRP_Injury + "," + incident.IRP_ClientGrievance + "," + incident.IRP_PhysicalRestraint + "," + incident.IRP_Seclusion + "," + incident.IRP_PropertyDamage +
                    "," + incident.IRP_PropertyMissing + "," + incident.IRP_Theft + "," + incident.IRP_Other + "," + incident.IRP_PoliceRepNo + "," + incident.IRP_RestraintSTTime + "," + incident.IRP_RestraintENTime + "," + incident.IRP_SeclusionSTTime +
                    "," + incident.IRP_SeclusionENTime + "," + incident.IRP_ContribAbuseAllegation + "," + incident.IRP_ContribPhysicalAggression + "," + incident.IRP_ContribPoliceInvolvement + "," + incident.IRP_ContribInjuryItems + "," + incident.IRP_ContribUnplannedHospitalization +
                    "," + incident.IRP_ContribSexualEncounter + "," + incident.IRP_ContribSeclusion + "," + incident.IRP_InjuryType + "," + incident.IRP_BodyPart + "," + incident.IRP_InjuryFollowUp); ;
            }

            String csv = sb.ToString();
            String reportName = "Incidents" + dateFrom.ToShortDateString() + "_" + dateTo.ToShortDateString() + ".csv";

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", reportName);
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

            //Create a viewbag (Viewbag.mgrPosition) to hold the mgrPosition of the current user
            //This viewbag is what's used to give the mgrPosition to the Edit view
            //for the function of diplayRiskManagerComment
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

            // Contributing Factors - 1. Abuse Allegation dropdown list items - Gina Chin Fatt
            List<SelectListItem> abuseAllegationItems = new List<SelectListItem>();
            abuseAllegationItems.Add(new SelectListItem { Value = "-1", Text = "Abuse Allegation", Selected = true, Disabled = true });
            abuseAllegationItems.Add(new SelectListItem {Value="Client/Client", Text="Client/Client"});
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Staff", Text = "Client/Staff" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Parent", Text = "Client/Parent" });
            abuseAllegationItems.Add(new SelectListItem { Value = "Client/Other", Text = "Client/Other" });

            ViewBag.abuseAllegationItems = abuseAllegationItems;

            // Contributing Factors - 2. Physical Aggression dropdown list items - Gina Chin Fatt
            List<SelectListItem> physicalAggressionItems = new List<SelectListItem>();
            physicalAggressionItems.Add(new SelectListItem { Value = "-1", Text = "Physical Restraint", Selected = true, Disabled = true });
            physicalAggressionItems.Add(new SelectListItem { Value = "Toward Others", Text = "Toward Others" });
            physicalAggressionItems.Add(new SelectListItem { Value = "Toward Self", Text = "Toward Self" });
 
            ViewBag.physicalAggressionItems = physicalAggressionItems;

            // Contributing Factors - 3. Police Involvement dropdown list items - Gina Chin Fatt
            List<SelectListItem> policeInvolvementItems = new List<SelectListItem>();
            policeInvolvementItems.Add(new SelectListItem { Value = "-1", Text = "Police/Fire/Rescue", Selected = true, Disabled = true });
            policeInvolvementItems.Add(new SelectListItem { Value = "Baker Act", Text = "Baker act" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Medical Emergency", Text = "Medical Emergency" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Elopement", Text = "Elopement" });
            policeInvolvementItems.Add(new SelectListItem { Value = "Criminal Activity", Text = "Criminal Activity" });
            policeInvolvementItems.Add(new SelectListItem { Value = "False Alarm", Text = "False Alarm" });
            
            ViewBag.policeInvolvementItems = policeInvolvementItems;

            // Contributing Factors - 4. Injury Items dropdown list items - Gina Chin Fatt
            List<SelectListItem> injuryItems = new List<SelectListItem>();
            injuryItems.Add(new SelectListItem { Value = "-1", Text = "Cause of Injury", Selected = true, Disabled = true});
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
            seclusionItems.Add(new SelectListItem { Value = "Toward Others", Text = "Toward Others" });
            seclusionItems.Add(new SelectListItem { Value = "Toward Self", Text = "Toward Self" });

            ViewBag.seclusionItems = seclusionItems;

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

            return View();
        }

        // GET: IncidentReports/Edit/5
        public ActionResult Edit(int? id)
        {

            IncidentReport incidentReportData = db.IncidentReports.Find(id);

            //Create a viewbag (Viewbag.mgrPosition) to hold the mgrPosition of the current user
            //This viewbag is what's used to give the mgrPosition to the Edit view
            //for the function of diplayRiskManagerComment
            ApplicationDbContext context = new ApplicationDbContext();
            ApplicationUser userPosition = context.Users.Where(m => m.UserName.Equals(User.Identity.Name)).FirstOrDefault();
            String mgrPosition = userPosition.mgrPosition;
            ViewBag.mgrPosition = mgrPosition;

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
            physicalAggressionItems.Add(new SelectListItem { Value = "Toward Others", Text = "Toward Others" });
            physicalAggressionItems.Add(new SelectListItem { Value = "Toward Self", Text = "Toward Self" });

            ViewBag.physicalAggressionItems = physicalAggressionItems;

            // Contributing Factors - 2a. Physical Restraint Times
            String IRP_RestraintSTTimeValue = incidentReportData.IRP_RestraintSTTime;
            String IRP_RestraintENTimeValue = incidentReportData.IRP_RestraintENTime;

            ViewBag.IRP_RestraintSTTimeValue = IRP_RestraintSTTimeValue;
            ViewBag.IRP_RestraintENTimeValue = IRP_RestraintENTimeValue;

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
            String IRP_PoliceRepNoValue = incidentReportData.IRP_PoliceRepNo;

            ViewBag.IRP_PoliceRepNoValue = IRP_PoliceRepNoValue;

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
            seclusionItems.Add(new SelectListItem { Value = "Toward Others", Text = "Toward Others" });
            seclusionItems.Add(new SelectListItem { Value = "Toward Self", Text = "Toward Self" });

            ViewBag.seclusionItems = seclusionItems;

            // Contributing Factors - 7a. Seclusion Times
            String IRP_SeclusionSTTimeValue = incidentReportData.IRP_SeclusionSTTime;
            String IRP_SeclusionENTimeValue = incidentReportData.IRP_SeclusionENTime;

            ViewBag.IRP_SeclusionSTTimeValue = IRP_SeclusionSTTimeValue;
            ViewBag.IRP_SeclusionENTimeValue = IRP_SeclusionENTimeValue;

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
            "IRP_ContribUnplannedHospitalization, " +
            "IRP_ContribSexualEncounter, " +
            "IRP_ContribSeclusion, " +
            "IRP_AbuseAllegation, " +
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

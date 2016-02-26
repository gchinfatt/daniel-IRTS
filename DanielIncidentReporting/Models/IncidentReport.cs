using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    [Table("IncidentReports")]
    public class IncidentReport
    {
        [Key]
        public int IRP_ID { get; set; }

        [Display(Name = "Nature of Incident")]
        public string IRP_Category { get; set; }

        [Display(Name = "Location")]
        public string IRP_Location { get; set; }
        
        [Display(Name = "Report Date")]
        public DateTime IRP_ReportDate { get; set; }

        [Display(Name = "Incident Date")]
        public DateTime IRP_IncidentDate { get; set; }

        [Display(Name = "Victim First Name")]
        public string IRP_VictimFirstName { get; set; }

        [Display(Name = "Victim Last Name")]
        public string IRP_VictimLastName { get; set; }

        [Display(Name = "Report On")]
        public string IRP_ReportOn { get; set; }

        [Display(Name = "Residential Manager Approved Date")]
        public DateTime IRP_ResMgrApprovedDate { get; set; }

        [Display(Name = "Department Director Approved Date")]
        public DateTime IRP_DeptDirApprovedDate { get; set; }

        [Display(Name = "Risk Manager Approved Date")]
        public DateTime IRP_RiskMgrApprovedDate { get; set; }

        [Display(Name = "Risk Manager Comment")]
        public string IRP_RiskMgrComment { get; set; }

        [Display(Name = "Prepared By First Name")]
        public string IRP_PreparedByFirstName { get; set; }

        [Display(Name = "Prepared By Last Name")]
        public string IRP_PreparedByLastName { get; set; }

        [Display(Name = "Description")]
        public string IRP_Description { get; set; }

        [Display(Name = "Injury Type")]
        public string IRP_InjuryType { get; set; }

        [Display(Name = "Body Part")]
        public string IRP_BodyPart { get; set; }

        [Display(Name = "Injury Follow Up")]
        public string IRP_InjuryFollowUp { get; set; }

        public string IRP_ApprovalLevelReq { get; set; }

        [Display(Name = "Program Name")]
        public string IRP_ProgramName { get; set; }
        [Display(Name = "Witness")]
        public string IRP_Witness { get; set; }
        [Display(Name = "Notified Person")]
        public string IRP_Notified { get; set; }

        //Contributing Factors
        public string IRP_Contrib1 { get; set; }
        public string IRP_Contrib2 { get; set; }
        public string IRP_Contrib3 { get; set; }
        public string IRP_Contrib4 { get; set; }
        //Incident types 
        public string IRP_AbuseAllegation { get; set; }
        public string IRP_Death { get; set; }
        public string IRP_PoliceFire { get; set; }
        public string IRP_SuicideGestures { get; set; }
        public string IRP_UnplannedHospitalization { get; set; }
        public string IRP_SexualEncounter { get; set; }
        public string IRP_SubstanceAbuse { get; set; }
        public string IRP_MedicationError { get; set; }
        public string IRP_Injury { get; set; }
        public string IRP_ClientGrievance { get; set; }
        public string IRP_PhysicalRestraint { get; set; }
        public string IRP_Seclusion { get; set; }
        public string IRP_PropertyDamage { get; set; }
        public string IRP_PropertyMissing { get; set; }
        public string IRP_Theft { get; set; }
        public string IRP_Other { get; set; }

        //Incident Types extras
        public string IRP_RestraintSTTime { get; set; }
        public string IRP_RestraintENTime { get; set; }
        public string IRP_SeclusionSTTime { get; set; }
        public string IRP_SeclusionENTime { get; set; }
        public string IRP_PoliceRepNo { get; set; }
        
    }
}
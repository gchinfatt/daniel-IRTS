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

        [Display(Name = "RiskMgrComment")]
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
        
        [Display(Name = "Contributing Factor")]
        public string IRP_ContribFactor { get; set; }
    }
}
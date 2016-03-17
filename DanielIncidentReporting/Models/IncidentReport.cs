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
        [Display(Name = "Incident Time")]
        public string IRP_IncidentTime { get; set; }

        [Display(Name = "Victim First Name")]
        public string IRP_VictimFirstName { get; set; }

        [Display(Name = "Victim Last Name")]
        public string IRP_VictimLastName { get; set; }

        [Display(Name = "Report On")]
        public string IRP_ReportOn { get; set; }

        [Display(Name = "Residential Manager")]
        public string IRP_ResMgrEmail { get; set; }
        [Display(Name = "Date")]
        public DateTime IRP_ResMgrApprovedDate { get; set; }
        [Display(Name = "Department Director")]
        public string IRP_DepDirEmail { get; set; }
        [Display(Name = "Date")]
        public DateTime IRP_DeptDirApprovedDate { get; set; }
        [Display(Name = "Risk Manager")]
        public string IRP_RiskMgrEmail { get; set; }
        [Display(Name = "Date")]
        public DateTime IRP_RiskMgrApprovedDate { get; set; }

        [Display(Name = "Risk Manager Comment")]
        public string IRP_RiskMgrComment { get; set; }

        [Display(Name = "Reporter First Name")]
        public string IRP_PreparedByFirstName { get; set; }

        [Display(Name = "Reporter Last Name")]
        public string IRP_PreparedByLastName { get; set; }

        [Display(Name = "Description")]
        public string IRP_Description { get; set; }

        [Display(Name = "Injury Type")]
        public string IRP_InjuryType { get; set; }

        [Display(Name = "Body Part")]
        public string IRP_BodyPart { get; set; }

        [Display(Name = "Injury Follow-Up")]
        public string IRP_InjuryFollowUp { get; set; }

        [Display(Name = "Approval Level Required")]
        public string IRP_ApprovalLevelReq { get; set; }

        [Display(Name = "Program Name")]
        public string IRP_ProgramName { get; set; }

        [Display(Name = "Witness")]
        public string IRP_Witness { get; set; }

        [Display(Name = "Notified Person")]
        public string IRP_Notified { get; set; }

        //Contributing Factors
        [Display(Name = "Abuse Allegation")]
        public string IRP_ContribAbuseAllegation { get; set; }

        [Display(Name = "Physical Restraint")]
        public string IRP_ContribPhysicalAggression { get; set; }

        [Display(Name = "Police Involvement")]
        public string IRP_ContribPoliceInvolvement { get; set; }

        [Display(Name = "Injury Items")]
        public string IRP_ContribInjuryItems { get; set; }

        [Display(Name = "Unplanned Hospitalization")]
        public string IRP_ContribUnplannedHospitalization { get; set; }

        [Display(Name = "Sexual Encounter")]
        public string IRP_ContribSexualEncounter { get; set; }

        [Display(Name = "Seclusion")]
        public string IRP_ContribSeclusion { get; set; }

        //Incident types 
        [Display(Name = "Abuse Allegation")]
        public string IRP_AbuseAllegation { get; set; }

        [Display(Name = "Death")]
        public string IRP_Death { get; set; }

        [Display(Name = "Involvement with Police/Fire")]
        public string IRP_PoliceFire { get; set; }
        [Display(Name = "AMA")]
        public string IRP_AMA { get; set; }

        [Display(Name = "Suicide Gestures")]
        public string IRP_SuicideGestures { get; set; }

        [Display(Name = "Unplanned Hospitalization")]
        public string IRP_UnplannedHospitalization { get; set; }

        [Display(Name = "Sexual Encounter")]
        public string IRP_SexualEncounter { get; set; }

        [Display(Name = "Substance Abuse")]
        public string IRP_SubstanceAbuse { get; set; }

        [Display(Name = "Medication Error")]
        public string IRP_MedicationError { get; set; }

        [Display(Name = "Injury")]
        public string IRP_Injury { get; set; }

        [Display(Name = "Client Grievance")]
        public string IRP_ClientGrievance { get; set; }

        [Display(Name = "Physical Restraint")]
        public string IRP_PhysicalRestraint { get; set; }

        [Display(Name = "Seclusion")]
        public string IRP_Seclusion { get; set; }

        [Display(Name = "Property Damage")]
        public string IRP_PropertyDamage { get; set; }

        [Display(Name = "Property Missing")]
        public string IRP_PropertyMissing { get; set; }

        [Display(Name = "Theft")]
        public string IRP_Theft { get; set; }

        [Display(Name = "Other")]
        public string IRP_Other { get; set; }

        //Incident Types extras
        [Display(Name = "Restraint Start Time")]
        public string IRP_RestraintSTTime { get; set; }

        [Display(Name = "Restraint End Time")]
        public string IRP_RestraintENTime { get; set; }

        [Display(Name = "Seclusion Start Time")]
        public string IRP_SeclusionSTTime { get; set; }

        [Display(Name = "Seclusion End Time")]
        public string IRP_SeclusionENTime { get; set; }

        [Display(Name = "Police Report Number")]
        public string IRP_PoliceRepNo { get; set; }
        
    }
}
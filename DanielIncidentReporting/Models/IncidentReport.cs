using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    public class IncidentReport
    {
        
        public int id { get; set; }
        [Display(Name = "Incident Date")]
        public  DateTime incidentDate { get; set; }
        [Display (Name="Program")]
        public string programName { get; set; }
        [Display(Name = "Location")]
        public string location { get; set; }
        [Display(Name = "Incident Type")]
        public string incidentType { get; set; }
        [Display(Name = "Reporter First Name")]
        public string reporterFirstName { get; set; }
        [Display(Name = "Reporter Last Name")]
        public string reporterLastName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }        
    }

    public class IRTSDBContext : DbContext
    {
        public DbSet<IncidentReport> IncidentReports { get; set; }
    }
}
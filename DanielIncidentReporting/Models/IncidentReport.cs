using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    public class IncidentReport
    {
        public int id { get; set; }
        public  DateTime incidentDate { get; set; }
        public string programName { get; set; }
        public string location { get; set; }
        public string incidentType { get; set; }
        public string reporterFirstName { get; set; }
        public string reporterLastName { get; set; }
        public string Description { get; set; }        
    }

    public class IRTSDBContext : DbContext
    {
        public DbSet<IncidentReport> IncidentReports { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DanielIncidentReporting.Models
{
    public class IRTSDBContext2 : DbContext
    {
        public DbSet<IncidentReport> IncidentReports { get; set; }
        public IncidentReport IncidentReport { get; set; }
        public DbSet<IncidentType> IncidentTypes { get; set; }
        public IncidentType IncidentType { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public Incident Incident { get; set; }
    }
}
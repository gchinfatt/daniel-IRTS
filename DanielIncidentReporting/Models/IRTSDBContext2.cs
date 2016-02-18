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
        public DbSet<IncidentType> IncidentTypes { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<InjuryFollowUp> InjuryFollowUps { get; set; }

    }
}
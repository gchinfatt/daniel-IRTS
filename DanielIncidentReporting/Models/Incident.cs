using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    [Table("Incidents")]
    public class Incident
    {
        [Key, Column(Order = 0)]
        [ForeignKey("IncidentReport")]
        public int IRP_ID { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("IncidentType")]
        public int INT_ID { get; set; }
        public string INC_PoliceReportNo { get; set; }
        public string INC_StartTime { get; set; }
        public string INC_EndTime { get; set; }

        public virtual IncidentReport IncidentReport { get; set; }
        public virtual IncidentType IncidentType { get; set; }
    }
}
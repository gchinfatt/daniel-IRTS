using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    [Table("IncidentTypes")]
    public class IncidentType
    {
            [Key]
            public int INT_ID { get; set; }
            public string INT_Name { get; set; }
            public string INT_Category { get; set; }
    }
}

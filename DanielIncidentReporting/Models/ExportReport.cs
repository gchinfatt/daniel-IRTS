using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace DanielIncidentReporting.Models
{
    public class ExportReport
    {
        [Display(Name = "Start Date")]
        [Required]
        public DateTime fromDate { get; set; }

        [Display(Name = "End Date")]
        [Required]
        public DateTime toDate { get; set; }

    }
}
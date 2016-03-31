using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    public class ExportReport
    {
        [Display(Name = "From")]
        //[Required(ErrorMessage = "* Date From is required")]
        public DateTime fromDate { get; set; }

        [Display(Name = "To")]
        //[Required(ErrorMessage = "* Date To is required")]
        public DateTime toDate { get; set; }
    }
}
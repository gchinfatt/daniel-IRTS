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
    [Table("Programs")]
    public class Program
    {
        [Key, Display(Name = "Program Name")]
        public int Prg_ID { get; set; }

        [Display(Name = "Program Name")]
        public string Prg_Name { get; set; }
        public string Prg_Active { get; set; }
    }
}
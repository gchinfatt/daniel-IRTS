using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DanielIncidentReporting.Models
{
    [Table("Programs")]
    public class Program
    {
        [Key]
        public int Prg_ID { get; set; }
        [Display(Name="Program Name")]
        public string Prg_Name { get; set; }
    }
}
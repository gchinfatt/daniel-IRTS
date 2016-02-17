using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DanielIncidentReporting.Models
{
    [Table("InjuryFollowUps")]
    public class InjuryFollowUp
    {
        [Key, Display(Name = "Injury Followup")]
        public int IFU_ID { get; set; }

        [Display(Name = "Injury Followup")]
        public string IFU_name { get; set; }
    }
}

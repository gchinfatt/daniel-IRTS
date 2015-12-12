using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DanielIncidentReporting.Controllers
{
    public class IncidentReportController : Controller
    {
        // GET: IncidentReport
        public ActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VetPharmacy.Controllers
{
    public class StatsController : Controller
    {
        // GET: Stats
        public ActionResult Medicine()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Controllers
{
    public class ObjectivesController : Controller
    {
        //
        // GET: /Objectives/....

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IterationObjectives()
        {
            return View();
        }

        public ActionResult MonthObjectives()
        {
            return View();
        }

        public ActionResult QuartalObjectives()
        {
            return View();
        }

    }
}

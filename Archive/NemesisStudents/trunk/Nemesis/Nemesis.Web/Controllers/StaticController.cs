using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Controllers
{
    public class StaticController : Controller
    {
        //
        // GET: /Static/

        public ActionResult About()
        {
          ViewBag.Message = "Your app description page.";

          return View();
        }

        public ActionResult Contact()
        {
          ViewBag.Message = "Your contact page.";

          return View();
        }

    }
}

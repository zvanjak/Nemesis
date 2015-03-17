using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.DAL;
using Nemesis.Web.Models;
using Nemesis.Services;

namespace Nemesis.Web.Controllers
{
    public class ActivityController : Controller
    {
        // GET: Activity
        public ActionResult Index()
        {
            using (var repo = new ActivityRepository(new NemesisContext()))
            {
                return View(
                    ServiceProvider.ActiviyService(repo).All().Select(
                        item => new ActivityViewModel(item)
                    ).ToList()
                );
            }
        }

        public ActionResult Today()
        {
            using (var repo = new ActivityRepository(new NemesisContext()))
            {
                return View(
                    "Index",
                    ServiceProvider.ActiviyService(repo).TodayActivities().Select(
                        item => new ActivityViewModel(item)
                    ).ToList()
                );
            }
        }

        public ActionResult CurrentWeek()
        {
            using (var repo = new ActivityRepository(new NemesisContext()))
            {
                return View(
                    "Index",
                    ServiceProvider.ActiviyService(repo).CurrentWeekActivities().Select(
                        item => new ActivityViewModel(item)
                    ).ToList()
                );
            }
        }

        public ActionResult Create()
        {
            return View(new ActivityCreateModel());
        }

        [HttpPost]
        public ActionResult Create(ActivityCreateModel model)
        {
            try
            {
                using (var repo = new ActivityRepository(new NemesisContext()))
                {
                    ServiceProvider.ActiviyService(repo).Create(model.CreateWorkActivity());
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
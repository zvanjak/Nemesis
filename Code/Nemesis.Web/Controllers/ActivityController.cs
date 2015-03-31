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

        public ActionResult CreateActivityPartial()
        {
            return PartialView("Partials/_CreateActivity", new ActivityCreateModel());
        }

        public JsonResult SelectWeekObjectives()
        {
            return SelectObjectives<WeekObjective>();
        }

        public JsonResult SelectMonthObjectives()
        {
            return SelectObjectives<MonthObjective>();
        }

        public JsonResult SelectQuartalObjectives()
        {
            return SelectObjectives<QuartalObjective>();
        }

        private JsonResult SelectObjectives<T>() where T : Objective
        {
            ICollection<Objective> objectives = ObjectiveService.GetObjectives<T>();

            List<Option> options = new List<Option>();

            foreach (Objective o in objectives)
            {
                Option j = new Option();
                j.Value = o.Id;
                j.Text = o.Display;
                options.Add(j);
            }

            return Json(options, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckedObjectives()
        {
            ActivityCreateModel model = new ActivityCreateModel();
            ICollection<Objective> objectives = ObjectiveService.GetObjectives<WeekObjective>();
            model.Objectives = new MultiSelectList(objectives, "Id", "Display");
            return PartialView("Partials/_CreateActivityWithObjectives", model);
        }

        public ActionResult CheckedWorkOrders()
        {
            ActivityCreateModel model = new ActivityCreateModel();
            ICollection<WorkOrder> workOrders = WorkOrderService.GetWorkOrders();

            model.WorkOrders = new MultiSelectList(workOrders, "Id", "Display");
            return PartialView("Partials/_CreateActivityWithWorkOrders", model);
        }

        public class Option
        {
            public int Value { get; set; }
            public string Text { get; set; }

        }

        [HttpPost]
        public ActionResult Create(ActivityCreateModel model)
        {
            try
            {
                using (var repo = new ActivityRepository(new NemesisContext()))
                {
                    WorkActivity workActivity = new WorkActivity();
                    workActivity.Title = model.Title;
                    workActivity.Description = model.Description;
                    workActivity.ActualDuration = model.ActualDuration;
                    workActivity.RealizedForObjective = ObjectiveService.GetObjective(model.RealizedForObjectiveId);
                    workActivity.Date = DateTime.Now;
                    ServiceProvider.ActiviyService(repo).Create(workActivity);
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
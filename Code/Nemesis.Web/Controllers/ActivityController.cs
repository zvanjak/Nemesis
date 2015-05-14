using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nemesis.Domain;
using Nemesis.DAL;
using Nemesis.Web.Models;
using Nemesis.Services;
using System.Globalization;

namespace Nemesis.Web.Controllers
{
    public class ActivityController : Controller
    {
        // GET: Activity
        public ActionResult Index()
        {
            ViewBag.Day = DateTime.Now.Date;
            ICollection<WorkActivity> activities = ActivityService.GetActivities();
            return View(activities);

        }

        public ActionResult Today()
        {
            ViewBag.Day = DateTime.Now.Date;
            ICollection<WorkActivity> activities = ActivityService.GetTodayActivities();
            return View("ShowDailyActivities", activities);
        }



        public ActionResult CurrentWeek()
        {
            var date = SetMonday(DateTime.Now);
            ViewBag.Day = date;
            ViewBag.EndWeek = date.AddDays(6);
            ICollection<WorkActivity> activities = ActivityService.GetCurrentWeekActivities();
            return View("ShowWeekActivities", activities);
        }

        public ActionResult PreviousWeek(DateTime day)
        {
            ICollection<WorkActivity> activities = ActivityService.GetPreviousWeekActivities(day);
            var date = SetMonday(day).AddDays(-7);
            ViewBag.Day = date;
            ViewBag.EndWeek = date.AddDays(6);
            return PartialView("Partials/_ShowWeekActivities", activities);
        }

        public ActionResult NextWeek(DateTime day)
        {
            ICollection<WorkActivity> activities = ActivityService.GetNextWeekActivities(day);
            var date = SetMonday(day).AddDays(7);
            ViewBag.Day = date;
            ViewBag.EndWeek = date.AddDays(6);
            return PartialView("Partials/_ShowWeekActivities", activities);

        }

        private DateTime SetMonday(DateTime now)
        {
            DayOfWeek day = now.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Monday:
                    now = now.AddDays(0);
                    break;
                case DayOfWeek.Tuesday:
                    now = now.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    now = now.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    now = now.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    now = now.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    now = now.AddDays(-5);
                    break;
                case DayOfWeek.Sunday:
                    now = now.AddDays(-6);
                    break;
            }
            return new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);

        }

        [HttpPost]
        public ActionResult GetDay(int? datepickerForm)
        {
            if (datepickerForm != null)
            {
                var day = datepickerForm.Value;
                ViewBag.PostedDate = datepickerForm.Value;
                ICollection<WorkActivity> activities = ActivityService.GetActivitiesByDay(Convert.ToDateTime(day));
                return PartialView("Partials/_ShowDailyActivities", activities);
            }
            return View();
        }


        public ActionResult PreviousDay(DateTime day)
        {
            ViewBag.Day = day.AddDays(-1);
            ICollection<WorkActivity> activities = ActivityService.GetActivitiesByDay(day.AddDays(-1));
            return PartialView("Partials/_ShowDailyActivities", activities);
        }
        public ActionResult NextDay(DateTime day)
        {

            ViewBag.Day = day.AddDays(1);
            ICollection<WorkActivity> activities = ActivityService.GetActivitiesByDay(day.AddDays(1));
            return PartialView("Partials/_ShowDailyActivities", activities);
        }
        public ActionResult GetDay(DateTime day)
        {
            ViewBag.Day = day;
            ICollection<WorkActivity> activities = ActivityService.GetActivitiesByDay(day);
            return View("Index", activities);
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
                    workActivity.Date = DateTime.Now;
                    ActivityService.Create(workActivity, model.RealizedForObjectiveId, model.WorkOrderId);
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
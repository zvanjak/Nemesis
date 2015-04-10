using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nemesis.Domain;
using Nemesis.Services;
using Nemesis.Web.Models.Objective;
using System.Globalization;

namespace Nemesis.Web.Controllers
{
    public class ObjectiveController : Controller
    {
        // GET: Objective
        public ActionResult Index()
        {
            return ShowObjectives<Objective>("");
        }

        #region Show Objective

        public ActionResult ShowWeekObjective(int id, string filter)
        {
            ViewBag.ChildObjectiveType = "Child";
            return ShowSingleObjective<WeekObjective>(id, filter);
        }

        public ActionResult ShowMonthObjective(int id, string filter)
        {
            ViewBag.ChildObjectiveType = "Week";
            return ShowSingleObjective<MonthObjective>(id, filter);
        }

        public ActionResult ShowQuartalObjective(int id, string filter)
        {
            ViewBag.ChildObjectiveType = "Month";
            return ShowSingleObjective<QuartalObjective>(id, filter);
        }


        private ActionResult ShowSingleObjective<T>(int id, string dateFilter) where T : Objective
        {
            ICollection<Objective> objectives;
            if (String.IsNullOrEmpty(dateFilter))
            {
                objectives = ObjectiveService.GetObjectives<T>();
            }
            else
            {
                objectives = ObjectiveService.GetObjectivesFor<T>(dateFilter);
                ViewBag.Filter = dateFilter;
            }

            ViewBag.Entries = objectives;
            ViewBag.ObjId = id;

            return View();
        }


        public ActionResult ShowWeekObjectives()
        {
            int currentWeek = ObjectiveService.GetCurrentWeek();
            ViewBag.CurrentWeek = currentWeek;

            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(currentWeek));
            return View();
        }

        


        public ActionResult ShowMonthObjectives(string datumFilter)
        {
            int currentMonth = ObjectiveService.GetCurrentMonth();
            ViewBag.MonthObjectives = ObjectiveService.GetObjectives<MonthObjective>(o => o.MonthOrdNum.Equals(currentMonth));
            ViewBag.CurrentMonth = currentMonth;
            return View();
        }

        public ActionResult ShowQuartalObjectives(string datumFilter)
        {
            int currentQuartal = ObjectiveService.GetCurrentQuartal();
            ViewBag.QuartalObjectives = ObjectiveService.GetObjectives<QuartalObjective>(o => o.QuartalOrdNum.Equals(currentQuartal));
            ViewBag.CurrentQuartal = currentQuartal;
            return View();
        }

        

        private ActionResult ShowObjectives<T>(string dateFilter) where T : Objective
        {
            ICollection<Objective> objectives;
            if (!String.IsNullOrEmpty(dateFilter))
            {
                objectives = ObjectiveService.GetObjectivesFor<T>(dateFilter);
                ViewBag.Filter = dateFilter;
            }
            else
            {
                objectives = ObjectiveService.GetObjectives<T>();
            }
            ViewBag.Entries = objectives;
            return View("ShowObjectives");
        }

        public ActionResult ToPreviousWeek(int currentWeek)
        {
            if (currentWeek > 1)
            {
                currentWeek--;
            }
            ViewBag.CurrentWeek = currentWeek;
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(currentWeek));
            return PartialView("ShowWeekObjectives");
        }

        public ActionResult ToNextWeek(int currentWeek)
        {
            if (currentWeek < ObjectiveService.GetNumberOfWeeksInYear())
            {
                currentWeek++;
            }
            ViewBag.CurrentWeek = currentWeek;
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(currentWeek));
            return PartialView("ShowWeekObjectives");

        }

        

        public ActionResult ToPreviousMonth(int currentMonth)
        {
            if (currentMonth > 1)
            {
                currentMonth--;
            }
            ViewBag.CurrentMonth = currentMonth;
            ViewBag.MonthObjectives = ObjectiveService.GetObjectives<MonthObjective>(o => o.MonthOrdNum.Equals(currentMonth));
            return PartialView("ShowMonthObjectives");
        }

        public ActionResult ToNextMonth(int currentMonth)
        {
            if (currentMonth < 12)
            {
                currentMonth++;
            }
            ViewBag.CurrentMonth = currentMonth;
            ViewBag.MonthObjectives = ObjectiveService.GetObjectives<MonthObjective>(o => o.MonthOrdNum.Equals(currentMonth));
            return PartialView("ShowMonthObjectives");

        }

        public ActionResult ToPreviousQuartal(int currentQuartal)
        {
            if (currentQuartal > 1)
            {
                currentQuartal--;
            }
            ViewBag.CurrentQuartal = currentQuartal;
            ViewBag.QuartalObjectives = ObjectiveService.GetObjectives<QuartalObjective>(o => o.QuartalOrdNum.Equals(currentQuartal));
            return PartialView("ShowQuartalObjectives");
        }

        public ActionResult ToNextQuartal(int currentQuartal)
        {
            if (currentQuartal < 4)
            {
                currentQuartal++;
            }
            ViewBag.CurrentQuartal = currentQuartal;
            ViewBag.QuartalObjectives = ObjectiveService.GetObjectives<QuartalObjective>(o => o.QuartalOrdNum.Equals(currentQuartal));
            return PartialView("ShowQuartalObjectives");

        }

        #endregion

        #region Create Objective

        //public ActionResult CreateWeekObjective()
        //{
        //    var model = new WeekObjectiveViewModel
        //    {
        //        ParentObjectives = GetParentObjectives<MonthObjective>(),
        //        TeamMembers = GetTeamMembers()
        //    };

        //    return View(model);
        //}

        public ActionResult CreateWeekObjective(int weekOrdNum)
        {
            var model = new WeekObjectiveViewModel()
            {
                WeekOrdNum = weekOrdNum,
                ParentObjectives = GetParentObjectives<MonthObjective>(),
                TeamMembers = GetTeamMembers()
            };

            return PartialView("Partials/CreateWeekObjectivePartial", model);
        }

        public ActionResult RedirectToShowWeek()
        {
            return RedirectToAction("ShowWeekObjectives", "Objective");
        }

        public ActionResult CreateMonthObjective(int monthOrdNum)
        {
            var model = new MonthObjectiveViewModel
            {
                MonthOrdNum = monthOrdNum,
                ParentObjectives = GetParentObjectives<QuartalObjective>(),
                TeamMembers = GetTeamMembers()
            };

            return PartialView("Partials/CreateMonthObjectivePartial", model);
        }

        private MultiSelectList GetParentObjectives<T>() where T : Objective
        {
            return new MultiSelectList(ObjectiveService.GetObjectives<T>(), "Id", "Display");
        }

        public ActionResult CreateQuartalObjective(int quartalOrdNum)
        {
            var model = new QuartalObjectiveViewModel
            {
                QuartalOrdNum = quartalOrdNum,
                ParentObjectives = new MultiSelectList(new List<Objective>()),
                TeamMembers = GetTeamMembers()
            };

            return PartialView("Partials/CreateQuartalObjectivePartial", model);
        }

        private MultiSelectList GetTeamMembers()
        {
            return new MultiSelectList(TeamMemberService.GetTeamMembers(), "Id", "Display");
        }

        [HttpPost]
        public ActionResult CreateWeekObjective(WeekObjectiveViewModel model)
        {
            var objective = new WeekObjective {WeekOrdNum = model.WeekOrdNum};
            CreateObjective(objective, model);
            return Json(new { value = "Week objective created!"});
        }

        [HttpPost]
        public ActionResult CreateMonthObjective(MonthObjectiveViewModel model)
        {

            var obj = new MonthObjective { MonthOrdNum = model.MonthOrdNum };
            CreateObjective(obj, model);
            return Json(new { value = "Month objective created!" });
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(QuartalObjectiveViewModel model)
        {
            var objective = new QuartalObjective { QuartalOrdNum = model.QuartalOrdNum };
            CreateObjective(objective, model);
            return Json(new { value = "Quartal objective created!" });
        }

        private void CreateObjective<T>(T objective, ObjectiveViewModel model) where T : Objective
        {
            objective.ShortDescription = model.Title;
            objective.Description = model.Description;
            objective.Priority = GetPriorityFromString(model.Priority);
            objective.EstimatedTime = model.EstimatedTime;
            objective.CreatedOn = DateTime.Now;

            ObjectiveService.Create(objective, model.ParentId, model.TeamMembersId);
        }

        private ObjectivePriority GetPriorityFromString(string priority)
        {
            if (Enum.IsDefined(typeof (ObjectivePriority), priority))
            {
                return (ObjectivePriority) Enum.Parse(typeof (ObjectivePriority), priority);
            }
            return ObjectivePriority.LOW;
        }

        #endregion
    }
}
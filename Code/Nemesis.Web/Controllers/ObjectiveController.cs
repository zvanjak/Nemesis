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
            int weekOrdNum = ObjectiveService.GetCurrentWeekOrdNum();
            ViewBag.WeekOrdNum = weekOrdNum;
            ViewBag.WeekStart = ObjectiveService.GetCurrentWeekStart();
            ViewBag.WeekEnd = ObjectiveService.GetCurrentWeekEnd();
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(weekOrdNum));
            return View();
        }

        public ActionResult ShowMonthObjectives()
        {
            int monthOrdNum = ObjectiveService.GetCurrentMonthOrdNum();
            ViewBag.MonthOrdNum = monthOrdNum;
            ViewBag.MonthStart = ObjectiveService.GetCurrentMonthStart();
            ViewBag.MonthEnd = ObjectiveService.GetCurrentMonthEnd();
            ViewBag.MonthObjectives = ObjectiveService.GetObjectives<MonthObjective>(o => o.MonthOrdNum.Equals(monthOrdNum));
            return View();
        }

        public ActionResult ShowQuartalObjectives()
        {
            int quartalOrdNum = ObjectiveService.GetCurrentQuartalOrdNum();
            ViewBag.QuartalOrdNum = quartalOrdNum;
            ViewBag.QuartalStart = ObjectiveService.GetCurrentQuartalStart();
            ViewBag.QuartalEnd = ObjectiveService.GetCurrentQuartalEnd();
            ViewBag.QuartalObjectives = ObjectiveService.GetObjectives<QuartalObjective>(o => o.QuartalOrdNum.Equals(quartalOrdNum));
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

        #endregion

        #region Navigation

        public ActionResult ToPreviousWeek(int weekOrdNum, DateTime weekStart, DateTime weekEnd)
        {
            weekOrdNum = ObjectiveService.PreviousWeekOrdNum(weekOrdNum, weekEnd.Year);
            ViewBag.WeekOrdNum = weekOrdNum;
            ViewBag.WeekStart = ObjectiveService.PreviousWeekStart(weekStart);
            ViewBag.WeekEnd = ObjectiveService.PreviousWeekEnd(weekEnd);
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(weekOrdNum));
            return PartialView("ShowWeekObjectives");
        }

        public ActionResult ToNextWeek(int weekOrdNum, DateTime weekStart, DateTime weekEnd)
        {
            weekOrdNum = ObjectiveService.NextWeekOrdNum(weekOrdNum, weekStart.Year);
            ViewBag.WeekOrdNum = weekOrdNum;
            ViewBag.WeekStart = ObjectiveService.NextWeekStart(weekStart);
            ViewBag.WeekEnd = ObjectiveService.NextWeekEnd(weekEnd);
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(weekOrdNum));
            return PartialView("ShowWeekObjectives");

        }



        public ActionResult ToPreviousMonth(int monthOrdNum, DateTime monthStart, DateTime monthEnd)
        {
            monthOrdNum = ObjectiveService.PreviousMonthOrdNum(monthOrdNum);
            ViewBag.MonthOrdNum = monthOrdNum;
            ViewBag.MonthStart = ObjectiveService.PreviousMonthStart(monthStart);
            ViewBag.MonthEnd = ObjectiveService.PreviousMonthEnd(monthEnd);
            ViewBag.MonthObjectives = ObjectiveService.GetObjectives<MonthObjective>(o => o.MonthOrdNum.Equals(monthOrdNum));
            return PartialView("ShowMonthObjectives");
        }

        public ActionResult ToNextMonth(int monthOrdNum, DateTime monthStart, DateTime monthEnd)
        {
            monthOrdNum = ObjectiveService.NextMonthOrdNum(monthOrdNum);
            ViewBag.MonthOrdNum = monthOrdNum;
            ViewBag.MonthStart = ObjectiveService.NextMonthStart(monthStart);
            ViewBag.MonthEnd = ObjectiveService.NextMonthEnd(monthEnd);
            ViewBag.MonthObjectives = ObjectiveService.GetObjectives<MonthObjective>(o => o.MonthOrdNum.Equals(monthOrdNum));
            return PartialView("ShowMonthObjectives");

        }

        public ActionResult ToPreviousQuartal(int quartalOrdNum, DateTime quartalStart, DateTime quartalEnd)
        {
            quartalOrdNum = ObjectiveService.PreviousQuartalOrdNum(quartalOrdNum);
            ViewBag.QuartalOrdNum = quartalOrdNum;
            ViewBag.QuartalStart = ObjectiveService.PreviousQuartalStart(quartalStart);
            ViewBag.QuartalEnd = ObjectiveService.PreviousQuartalEnd(quartalEnd);
            ViewBag.QuartalObjectives = ObjectiveService.GetObjectives<QuartalObjective>(o => o.QuartalOrdNum.Equals(quartalOrdNum));
            return PartialView("ShowQuartalObjectives");
        }

        public ActionResult ToNextQuartal(int quartalOrdNum, DateTime quartalStart, DateTime quartalEnd)
        {
            quartalOrdNum = ObjectiveService.NextQuartalOrdNum(quartalOrdNum);
            ViewBag.QuartalOrdNum = quartalOrdNum;
            ViewBag.QuartalStart = ObjectiveService.NextQuartalStart(quartalStart);
            ViewBag.QuartalEnd = ObjectiveService.NextQuartalEnd(quartalEnd);
            ViewBag.QuartalObjectives = ObjectiveService.GetObjectives<QuartalObjective>(o => o.QuartalOrdNum.Equals(quartalOrdNum));
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
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nemesis.Domain;
using Nemesis.Services;
using Nemesis.Web.Models.Objective;

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
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(1));
            ViewBag.CurrentWeek = 1;
            return View();
        }


        public ActionResult ShowMonthObjectives(string datumFilter)
        {
            if (String.IsNullOrEmpty(datumFilter)) datumFilter = "";
            ViewBag.ObjectiveType = "Month";
            return ShowObjectives<MonthObjective>(datumFilter);
        }

        public ActionResult ShowQuartalObjectives(string datumFilter)
        {
            if (String.IsNullOrEmpty(datumFilter)) datumFilter = "";
            ViewBag.ObjectiveType = "Quartal";
            return ShowObjectives<QuartalObjective>(datumFilter);
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
            if (currentWeek < 50)
            {
                currentWeek++;
            }
            ViewBag.CurrentWeek = currentWeek;
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>(o => o.WeekOrdNum.Equals(currentWeek));
            return PartialView("ShowWeekObjectives");

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

        public ActionResult CreateWeekObjective()
        {
            var model = new WeekObjectiveViewModel()
            {
                ParentObjectives = GetParentObjectives<MonthObjective>(),
                TeamMembers = GetTeamMembers()
            };

            return PartialView("Partials/CreateWeekObjectivePartial", model);
        }

        public ActionResult CreateMonthObjective()
        {
            var model = new MonthObjectiveViewModel
            {
                ParentObjectives = GetParentObjectives<QuartalObjective>(),
                TeamMembers = GetTeamMembers()
            };

            return View(model);
        }

        private MultiSelectList GetParentObjectives<T>() where T : Objective
        {
            return new MultiSelectList(ObjectiveService.GetObjectives<T>(), "Id", "Display");
        }

        public ActionResult CreateQuartalObjective()
        {
            var model = new QuartalObjectiveViewModel
            {
                ParentObjectives = new MultiSelectList(new List<Objective>()),
                TeamMembers = GetTeamMembers()
            };

            return View(model);
        }

        private MultiSelectList GetTeamMembers()
        {
            return new MultiSelectList(TeamMemberService.GetTeamMembers(), "Id", "Display");
        }

        [HttpPost]
        public ActionResult CreateWeekObjective(WeekObjectiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var objective = new WeekObjective {WeekOrdNum = model.WeekOrdNum};
                CreateObjective(objective, model);
                return RedirectToAction("Index", "Home");
            }
            model.ParentObjectives = GetParentObjectives<MonthObjective>();
            model.TeamMembers = GetTeamMembers();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMonthObjective(MonthObjectiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var obj = new MonthObjective {MonthOrdNum = model.MonthOrdNum};
                CreateObjective(obj, model);
                return RedirectToAction("Index", "Home");
            }
            model.ParentObjectives = GetParentObjectives<QuartalObjective>();
            model.TeamMembers = GetTeamMembers();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(QuartalObjectiveViewModel model)
        {
            if (ModelState.IsValid)
            {
                var objective = new QuartalObjective {QuartalOrdNum = model.QuartalOrdNum};
                CreateObjective(objective, model);
                return RedirectToAction("Index", "Home");
            }
            model.ParentObjectives = new MultiSelectList(new List<Objective>());
            model.TeamMembers = GetTeamMembers();
            return View(model);
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
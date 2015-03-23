using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nemesis.DAL;
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


        private ActionResult ShowSingleObjective<T>(int id, string filter) where T : Objective
        {
            using (var repo = new GenericRepository<T>(new NemesisContext()))
            {
                List<Objective> objectives;
                if (String.IsNullOrEmpty(filter))
                {
                    objectives = repo.Get().ToList<Objective>();
                }
                else
                {
                    objectives =
                        repo.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(filter)).ToList<Objective>();
                    ViewBag.Filter = filter;
                }
                foreach (var obj in objectives)
                {
                    obj.AssignedToTeamMembers = obj.AssignedToTeamMembers;
                    obj.Objectives = obj.Objectives;
                }
                ViewBag.Entries = objectives;
            }
            ViewBag.ObjId = id;

            return View();
        }


        public ActionResult ShowWeekObjectives()
        {
            ViewBag.WeekObjectives = ObjectiveService.GetObjectives<WeekObjective>();
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

        #endregion

        #region Create Objective

        public ActionResult CreateWeekObjective()
        {
            var model = new WeekObjectiveViewModel();

            model.ParentObjectives = GetParentObjectives<MonthObjective>();
            model.TeamMembers = GetTeamMembers();
            return View(model);
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
            var model = new QuartalObjectiveViewModel();

            model.ParentObjectives = new MultiSelectList(new List<Objective>());
            model.TeamMembers = GetTeamMembers();
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
        public ActionResult CreateMonthObjective(MonthObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var obj = new MonthObjective {MonthOrdNum = entry.MonthOrdNum};
                CreateObjective(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            entry.ParentObjectives = GetParentObjectives<QuartalObjective>();
            entry.TeamMembers = GetTeamMembers();
            return View(entry);
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(QuartalObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var objective = new QuartalObjective();
                objective.QuartalOrdNum = entry.QuartalOrdNum;
                CreateObjective(objective, entry);
                return RedirectToAction("Index", "Home");
            }
            entry.ParentObjectives = new MultiSelectList(new List<Objective>());
            entry.TeamMembers = GetTeamMembers();
            return View(entry);
        }


        private void CreateObjective(Objective objective, ObjectiveViewModel model)
        {
            objective.Parent = ObjectiveService.GetObjective(model.ParentId);
            objective.ShortDescription = model.Title;
            objective.Description = model.Description;
            objective.Priority = GetPriorityFromString(model.Priority);
            objective.EstimatedTime = model.EstimatedTime;
            objective.CreatedOn = DateTime.Now;
            objective.AssignedToTeamMembers = TeamMemberService.GetTeamMembers(model.TeamMembersId);

            ObjectiveService.Save(objective);
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
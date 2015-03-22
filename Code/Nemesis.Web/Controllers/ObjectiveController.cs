using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.DAL;

using Nemesis.Web.Models;
using Nemesis.DAL.Repositories;

namespace Nemesis.Web.Controllers
{
    public class ObjectiveController : Controller
    {
        // GET: Objective
        public ActionResult Index()
        {
            return ShowObjectives<Objective>("");
        }

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
            using (NemesisContext nc = new NemesisContext())
            {
                using (var repo = new GenericRepository<T>(nc))
                {
                    List<Objective> listObj = new List<Objective>();
                    if (String.IsNullOrEmpty(filter))
                    {
                        listObj = repo.Get().ToList<Objective>();

                    }
                    else
                    {
                        listObj = repo.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(filter)).ToList<Objective>();
                        ViewBag.Filter = filter;
                    }
                    foreach (Objective obj in listObj)
                    {
                        obj.AssignedToTeamMembers = obj.AssignedToTeamMembers;
                        obj.Objectives = obj.Objectives;
                    }
                    ViewBag.Entries = listObj;
                }
            }
            ViewBag.ObjId = id;

            return View();
        }

        public ActionResult ShowWeekObjectives(string datumFilter)
        {
            if (String.IsNullOrEmpty(datumFilter)) datumFilter = "";
            ViewBag.ObjectiveType = "Week";
            return ShowObjectives<WeekObjective>(datumFilter);

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

        private ActionResult ShowObjectives<T>(string filter) where T : Objective
        {
            using (NemesisContext context = new NemesisContext())
            {
                using (var repo = new GenericRepository<T>(context))
                {
                    IEnumerable<Objective> listObj = new List<Objective>();
                    if (!String.IsNullOrEmpty(filter))
                    {
                        listObj = repo.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(filter));
                        ViewBag.Filter = filter;
                    }
                    else
                    {
                        listObj = repo.Get();
                    }
                    ViewBag.Entries = listObj;
                }
            }
            return View("ShowObjectives");
        }

        public ActionResult CreateWeekObjective()
        {
            ObjectiveViewModel model = new ObjectiveViewModel();

            model.ParentObjectives = GetParentObjectives<MonthObjective>();
            model.TeamMembers = GetTeamMembers();
            return View("CreateObjective", model);
        }

        public ActionResult CreateMonthObjective()
        {
            ObjectiveViewModel model = new ObjectiveViewModel();

            model.ParentObjectives = GetParentObjectives<QuartalObjective>();
            model.TeamMembers = GetTeamMembers();
            return View("CreateObjective", model);
        }

        public ActionResult CreateQuartalObjective()
        {
            ObjectiveViewModel model = new ObjectiveViewModel();

            model.ParentObjectives = new MultiSelectList(new List<Objective>());
            model.TeamMembers = GetTeamMembers();
            return View("CreateObjective", model);
        }

        private MultiSelectList GetTeamMembers()
        {
            IEnumerable<TeamMember> teamMembers = null;

            using (var repo = new GenericRepository<TeamMember>(new NemesisContext()))
            {
                teamMembers = repo.Get();
            }

            return new MultiSelectList(teamMembers,"Id","Display");
        }

        private MultiSelectList GetParentObjectives<T>()
            where T : Objective
        {
            IEnumerable<Objective> objectives = null;

            using (var repo = new GenericRepository<T>(new NemesisContext()))
            {
                objectives = repo.Get();
            }

            return new MultiSelectList(objectives, "Id", "Display");
        }

        [HttpPost]
        public ActionResult CreateWeekObjective(ObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                WeekObjective obj = new WeekObjective();
                CreateObjective<WeekObjective>(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = GetParentObjectives<MonthObjective>();
                entry.TeamMembers = GetTeamMembers();
                return View("CreateObjective", entry);
            }
        }

        [HttpPost]
        public ActionResult CreateMonthObjective(ObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                MonthObjective obj = new MonthObjective();
                CreateObjective<MonthObjective>(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = GetParentObjectives<QuartalObjective>();
                entry.TeamMembers = GetTeamMembers();
                return View("CreateObjective", entry);
            }
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(ObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                QuartalObjective obj = new QuartalObjective();
                CreateObjective<QuartalObjective>(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = new MultiSelectList(new List<Objective>());
                entry.TeamMembers = GetTeamMembers();
                return View("CreateObjective", entry);
            }
        }


        private void CreateObjective<T>(Objective obj, ObjectiveViewModel entry)
            where T : Objective
        {
            using (var context = new NemesisContext())
            {
                using (var membersRepo = new GenericRepository<TeamMember>(context))
                using (var objRepo = new GenericRepository<Objective>(context))
                {
                    obj.Parent = objRepo.GetByID(entry.ParentId);
                    obj.ShortDescription = entry.Title;
                    obj.Description = entry.Description;
                    obj.Priority = GetPriorityFromString(entry.Priority);
                    obj.EstimatedTime = entry.EstimatedTime;

                    List<TeamMember> members = new List<TeamMember>();

                    foreach (int id in entry.TeamMembersId)
                    {
                        TeamMember member = membersRepo.GetByID(id);
                        members.Add(member);
                    }

                    obj.AssignedToTeamMembers = members;

                    obj.CreatedOn = DateTime.Now;

                    objRepo.Insert(obj);
                    objRepo.Save();
                }
            } 
            
        }

        private ObjectivePriority GetPriorityFromString(string p)
        {
            if (Enum.IsDefined(typeof(ObjectivePriority), p))
            {
                return (ObjectivePriority)Enum.Parse(typeof(ObjectivePriority), p);
            }
            return ObjectivePriority.LOW;
        }
    }
}
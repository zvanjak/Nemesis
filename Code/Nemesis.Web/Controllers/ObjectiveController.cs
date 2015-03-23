using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.DAL;

using Nemesis.Web.Models.Objective;
using Nemesis.Services;

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

        #endregion

        #region Create Objective

        public ActionResult CreateWeekObjective()
        {
            WeekObjectiveViewModel model = new WeekObjectiveViewModel();

            model.ParentObjectives = GetParentObjectives<MonthObjective>();
            model.TeamMembers = GetTeamMembers();
            return View(model);
        }

        public ActionResult CreateMonthObjective()
        {
            MonthObjectiveViewModel model = new MonthObjectiveViewModel();

            model.ParentObjectives = GetParentObjectives<QuartalObjective>();
            model.TeamMembers = GetTeamMembers();
            return View(model);
        }

        public ActionResult CreateQuartalObjective()
        {
            QuartalObjectiveViewModel model = new QuartalObjectiveViewModel();

            model.ParentObjectives = new MultiSelectList(new List<Objective>());
            model.TeamMembers = GetTeamMembers();
            return View(model);
        }

        private MultiSelectList GetTeamMembers()
        {
            IEnumerable<TeamMember> teamMembers = null;

            using (var repo = new GenericRepository<TeamMember>(new NemesisContext()))
            {
                teamMembers = repo.Get();
            }

            return new MultiSelectList(teamMembers, "Id", "Display");
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
        public ActionResult CreateWeekObjective(WeekObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                WeekObjective obj = new WeekObjective();
                obj.WeekOrdNum = entry.WeekOrdNum;
                CreateObjective<WeekObjective>(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = GetParentObjectives<MonthObjective>();
                entry.TeamMembers = GetTeamMembers();
                return View(entry);
            }
        }

        [HttpPost]
        public ActionResult CreateMonthObjective(MonthObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                MonthObjective obj = new MonthObjective();
                obj.MonthOrdNum = entry.MonthOrdNum;
                CreateObjective<MonthObjective>(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = GetParentObjectives<QuartalObjective>();
                entry.TeamMembers = GetTeamMembers();
                return View(entry);
            }
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(QuartalObjectiveViewModel entry)
        {
            if (ModelState.IsValid)
            {
                QuartalObjective obj = new QuartalObjective();
                obj.QuartalOrdNum = entry.QuartalOrdNum;
                CreateObjective<QuartalObjective>(obj, entry);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = new MultiSelectList(new List<Objective>());
                entry.TeamMembers = GetTeamMembers();
                return View(entry);
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

        #endregion
    }
}
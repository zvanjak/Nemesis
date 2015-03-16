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

        public ActionResult Details(int id, string filter)
        {
            using (NemesisContext nc = new NemesisContext())
            {
                using (var repo = new GenericRepository<Objective>(nc))
                {
                    if (String.IsNullOrEmpty(filter))
                    {
                        IEnumerable<Objective> listObj = repo.Get();
                        ViewBag.Entries = listObj;
                    }
                    else
                    {
                        IEnumerable<Objective> listObj = repo.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(filter));
                        ViewBag.Entries = listObj;
                        ViewBag.Filter = filter;
                    }

                }
            }
            ViewBag.ObjId = id;

            return View();
        }

        public ActionResult ShowWeekObjectives(string datumFilter)
        {
            if (!String.IsNullOrEmpty(datumFilter))
            {
                return ShowObjectives<WeekObjective>(datumFilter);
            }
            else
            {
                return ShowObjectives<WeekObjective>("");
            }

        }

        public ActionResult ShowMonthObjectives()
        {
            return ShowObjectives<MonthObjective>("");
        }

        public ActionResult ShowQuartalObjectives()
        {
            return ShowObjectives<QuartalObjective>("");
        }

        private ActionResult ShowObjectives<T>(string filter) where T : Objective
        {
            using (NemesisContext context = new NemesisContext())
            {
                using (var repo = new GenericRepository<T>(context))
                {
                    if (!String.IsNullOrEmpty(filter))
                    {
                        IEnumerable<Objective> listObj = repo.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(filter));
                        ViewBag.Entries = listObj;
                        ViewBag.Filter = filter;
                    }
                    else
                    {
                        IEnumerable<Objective> listObj = repo.Get();
                        ViewBag.Entries = listObj;
                    }


                }
            }
            return View();
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

            model.ParentObjectives = new List<SelectListItem>();
            model.TeamMembers = GetTeamMembers();
            return View("CreateObjective", model);
        }

        private IEnumerable<SelectListItem> GetTeamMembers()
        {
            IEnumerable<TeamMember> teamMembers = null;

            using (var repo = new GenericRepository<TeamMember>(new NemesisContext()))
            {
                teamMembers = repo.Get();
            }

            return ToSelectList(teamMembers);
        }

        private IEnumerable<SelectListItem> GetParentObjectives<T>() 
            where T : Objective
        {
            IEnumerable<Objective> objectives = null;

            using (var repo = new GenericRepository<T>(new NemesisContext()))
            {
                objectives = repo.Get();
            }

            return ToSelectList(objectives);
        }

        private IEnumerable<SelectListItem> ToSelectList<T>(IEnumerable<T> objectives)
        {
            return new SelectList(objectives, "Id", "Display");
        }

        [HttpPost]
        public ActionResult CreateWeekObjective(ObjectiveViewModel entry)
        {
            WeekObjective obj = new WeekObjective();
            return CreateObjective<WeekObjective>(obj, entry);
        }

        [HttpPost]
        public ActionResult CreateMonthObjective(ObjectiveViewModel entry) 
        {
            MonthObjective obj = new MonthObjective();
            return CreateObjective<MonthObjective>(obj, entry);
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(ObjectiveViewModel entry)
        {
            QuartalObjective obj = new QuartalObjective();
            return CreateObjective<QuartalObjective>(obj, entry);
        }


        private ActionResult CreateObjective<T>(Objective obj, ObjectiveViewModel entry) 
            where T : Objective
        {
            if (ModelState.IsValid)
            {
                using (var objRepo = new GenericRepository<Objective>(new NemesisContext()))
                {
                    obj.Parent = objRepo.GetByID(entry.ParentId);
                    obj.ShortDescription = entry.Title;
                    obj.Description = entry.Description;
                    obj.Priority = GetPriorityFromString(entry.Priority);
                    obj.EstimatedTime = entry.EstimatedTime;

                    List<TeamMember> members = new List<TeamMember>();

                    using (var membersRepo = new GenericRepository<TeamMember>(new NemesisContext()))
                    {
                        members.Add(membersRepo.GetByID(entry.LeaderId));
                    }

                    obj.AssignedToTeamMembers = members;

                    obj.CreatedOn = DateTime.Now;

                    objRepo.Insert(obj);
                    objRepo.Save();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                entry.ParentObjectives = GetParentObjectives<T>();
                entry.TeamMembers = GetTeamMembers();
                return View("CreateObjective", entry);
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
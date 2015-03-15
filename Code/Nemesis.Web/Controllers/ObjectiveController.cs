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
            using (var repo = new GenericRepository<Objective>(new NemesisContext()))
			{
				IList<Objective> listObj = repo.Get().ToList();
                ViewBag.Entries = listObj;
            }
            return View();
        }

        public ActionResult ShowWeekObjectives()
        {
            using(NemesisContext context = new NemesisContext())
            {
                using (ObjectiveRepository repo = new ObjectiveRepository(context))
                {
                    IList<Objective> listObj = repo.Get().Where(x => x.GetType() == typeof(WeekObjective)).ToList();
                    ViewBag.Entries = listObj;
                }
            }
            return View();
        }

        public ActionResult ShowMonthObjectives()
        {
            using (NemesisContext context = new NemesisContext())
            {
                using (ObjectiveRepository repo = new ObjectiveRepository(context))
                {
                    IList<Objective> listObj = repo.Get().Where(x => x.GetType() == typeof(MonthObjective)).ToList();
                    ViewBag.Entries = listObj;
                }
            }
            return View();
        }

        public ActionResult ShowQuartalObjectives()
        {
            using (NemesisContext context = new NemesisContext())
            {
                using (ObjectiveRepository repo = new ObjectiveRepository(context))
                {
                    IList<Objective> listObj = repo.Get().Where(x => x.GetType() == typeof(QuartalObjective)).ToList();
                    ViewBag.Entries = listObj;
                }
            }
            return View();
        }

        public ActionResult CreateWeekObjective()
        {
            return CreateObjective();
        }

        public ActionResult CreateMonthObjective()
        {
            return CreateObjective();
        }

        public ActionResult CreateQuartalObjective()
        {
            return CreateObjective();
        }

        private ActionResult CreateObjective()
        {
            ObjectiveViewModel model = new ObjectiveViewModel();

            model.ParentObjectives = GetParentObjectives();
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

        private IEnumerable<SelectListItem> GetParentObjectives()
        {
            IEnumerable<Objective> objectives = null;

            using (var repo = new ObjectiveRepository(new NemesisContext()))
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
            return CreateObjective(obj, entry);
        }

        [HttpPost]
        public ActionResult CreateMonthObjective(ObjectiveViewModel entry) 
        {
            MonthObjective obj = new MonthObjective();
            return CreateObjective(obj, entry);
        }

        [HttpPost]
        public ActionResult CreateQuartalObjective(ObjectiveViewModel entry)
        {
            QuartalObjective obj = new QuartalObjective();
            return CreateObjective(obj, entry);
        }


        private ActionResult CreateObjective(Objective obj, ObjectiveViewModel entry)
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
                entry.ParentObjectives = GetParentObjectives();
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
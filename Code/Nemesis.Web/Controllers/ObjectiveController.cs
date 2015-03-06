using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.DAL;

using Nemesis.Web.Models;

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

				public ActionResult CreateWeekObjective()
				{
					return View();

				}
				
				[HttpPost]		
				public ActionResult CreateWeekObjective(ObjectiveViewModel entry)
				{
					using (var repo = new GenericRepository<Objective>(new NemesisContext()))
					{
						Objective obj = new Objective();

						obj.Title = entry.Title;
						obj.Description = entry.Description;

						obj.CreatedOn = DateTime.Now;
						obj.Priority = ObjectivePriority.HIGH;
						//obj.AssignedToTeam = new Team() { Name = "TestTeam23" };
						//            obj.AssignedToTeam = new Team() { Name = "TestTeam", TeamLeader = new TeamMember() { Name = "TestTeam leader" } };

						repo.Insert(obj);
						repo.Save();
					}

					using (var repo = new GenericRepository<Team>(new NemesisContext()))
					{
						Team obj = new Team();
						obj.Name = "Prdo";

						repo.Insert(obj);
						repo.Save();
					}
					return View();
				}
		}
}
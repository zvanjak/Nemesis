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
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            using (var repo = new GenericRepository<Team>(new NemesisContext())) {
                IList<Team> teams = repo.Get().ToList();

                IList<TeamViewModel> teamsView = new List<TeamViewModel>();
                foreach (Team t in teams) {
                    teamsView.Add(new TeamViewModel(t));
                }

                return View(teamsView);
            }
        }

        //// GET: Team/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Team/Create
        public ActionResult Create()
        {
            
            
            TeamCreateModel m = new TeamCreateModel();

            return View(m);
        }

        // POST: Team/Create
        [HttpPost]
        public ActionResult Create(TeamCreateModel model)
        {
            try
            {
                // TODO: Add insert logic here

                return View(model);
            }
            catch
            {
                return View();
            }
        }

        //// GET: Team/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Team/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Team/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Team/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}

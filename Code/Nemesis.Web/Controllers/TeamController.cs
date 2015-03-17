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
        public ActionResult Create(TeamCreateModel model, String finishedSubmitButton, string continueSubmitting)
        {
            try
            {
                NemesisContext c = new NemesisContext();
                // TODO: Add insert logic here
                if (finishedSubmitButton != null) {
                    Team newTeam = new Team();
                    using (var memberRepo = new GenericRepository<TeamMember>(c)) {

                        newTeam.Name = model.Name;
                        newTeam.Type = (TeamTypes) Enum.Parse(typeof(TeamTypes), model.TeamType);

                        // add members to team
                        foreach (String memberID in model.Members) {
                            int id = int.Parse(memberID);
                            TeamMember member = memberRepo.GetByID(id);

                            newTeam.Members.Add(member);
                            member.MemberOfTheTeam = newTeam;
                        }

                        newTeam.Leader = memberRepo.GetByID(int.Parse(model.TeamLeader));
                    }
                        // add subteams to team
                    using (var teamRepo = new GenericRepository<Team>(c)) {
                        foreach (String teamID in model.Subteams) {
                            int id = int.Parse(teamID);
                            newTeam.SubTeams.Add(teamRepo.GetByID(id));
                        }

                        newTeam.Parent = teamRepo.GetByID(int.Parse(model.ParentTeam));

                        teamRepo.Insert(newTeam);
                        teamRepo.Save();
                    }

                    return RedirectToAction("Index");
                }
                else if (continueSubmitting != null) {
                    if (model.CurrentSubteam != null) {
                        model.Subteams.Add(model.CurrentSubteam);
                    }

                    if (model.CurrentMember != null) {
                        model.Members.Add(model.CurrentMember);
                    }

                    return View(model);
                }
                else {
                    throw new Exception("How did we get here? Unknown submit button clicked");
                }
            }
            catch
            {
                return View(model);
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

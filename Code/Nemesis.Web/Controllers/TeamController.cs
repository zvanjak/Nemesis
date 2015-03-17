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
        public ActionResult Create(TeamCreateModel model, string finishedSubmitButton, string submitSubteam, string submitMember)
        {
            try
            {
                // Save the new team
                if (finishedSubmitButton != null) {
                    SaveNewTeam(model);

                    return RedirectToAction("Index");
                }
                else if (submitSubteam != null) {
                    if (model.CurrentSubteam != null) {
                        model.SubteamIDs.Add(model.CurrentSubteam);
                        model.SubteamNames.Add(model.TeamItems.Where(i => i.Value == model.CurrentSubteam).First().Text);
                    }

                    return View(model);
                }
                else if (submitMember != null) {
                    if (model.CurrentMember != null) {
                        model.MemberIDs.Add(model.CurrentMember);
                        model.MemberNames.Add(model.MemberItems.Where(i => i.Value == model.CurrentMember).First().Text);
                    }

                    return View(model);
                }
                else {
                    throw new Exception("How did we get here? Unknown submit button clicked");
                }
            }
            catch
            {
                return View();
            }
        }

        private static void SaveNewTeam(TeamCreateModel model)
        {
            NemesisContext c = new NemesisContext();
            Team newTeam = new Team();
            using (var memberRepo = new GenericRepository<TeamMember>(c)) {

                newTeam.Name = model.Name;
                newTeam.Type = (TeamTypes)Enum.Parse(typeof(TeamTypes), model.TeamType);

                // add members to team
                foreach (String memberID in model.MemberIDs) {
                    int id = int.Parse(memberID);
                    TeamMember member = memberRepo.GetByID(id);

                    newTeam.Members.Add(member);
                    member.MemberOfTheTeam = newTeam;

                    memberRepo.Update(member);
                }

                newTeam.Leader = memberRepo.GetByID(int.Parse(model.TeamLeader));



                // add subteams to team
                using (var teamRepo = new GenericRepository<Team>(c)) {
                    foreach (String teamID in model.SubteamIDs) {
                        int id = int.Parse(teamID);
                        newTeam.SubTeams.Add(teamRepo.GetByID(id));
                    }

                    newTeam.Parent = teamRepo.GetByID(int.Parse(model.ParentTeam));

                    teamRepo.Insert(newTeam);
                    teamRepo.Save();
                    memberRepo.Save();
                }
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

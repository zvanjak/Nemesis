using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.Entity;

using Nemesis.DAL;
using Nemesis.Domain;
using System.Collections.Generic;
using Nemesis.Domain.Assets;

namespace Nemesis.DAL.Test
{
  [TestClass]
  public class TeamTests
  {
    [TestMethod]
    public void TestAddNewTeam()
    {
        using (var repo = new GenericRepository<Team>(new NemesisContext()))
        {
            Database.SetInitializer(new NemesisInitializer());

            TeamMember mem = new TeamMember() {Name = "Test team leader"};

            var newTeam = new Team { Name = "Testni tim 8", TeamLeader = mem};

            repo.Insert(newTeam);
            repo.Save();
        }
    }

    [TestMethod]
    public void TestAddTeamHierarchy()
    {
        using (var repo = new GenericRepository<Team>(new NemesisContext()))
        {
            //Database.SetInitializer(new NemesisInitializer());

            //var direktor = new TeamMember() {Name = "Direktor"};
            //var voditelj = new TeamMember() {Name = "Voditelj odjela"};
            ////var firm = new Team { Name = "Firma", Type = TeamType.Company, Parent = null, TeamLeader = direktor};
            ////var team1 = new Team() {Name = "Odjel 1", Type = TeamType.Sector, Parent = firm, TeamLeader = voditelj};

            //firm.TeamLeader = direktor;
            //firm.TeamMembers.Add(direktor);

            //repo.Insert(firm);
            //repo.Insert(team1);
            //repo.Save();
        }
    }

    [TestMethod]
    public void TestGetTeams()
    {
      using (var repo = new GenericRepository<Team>(new NemesisContext()))
      {
        Database.SetInitializer(new NemesisInitializer());

        List<Team> teams = (List<Team>)repo.Get(t => t.Name.Equals("Testni tim 8"));

        Assert.AreEqual(4, teams.Count);

      }
    }

    //[TestMethod]
    //public void TestAddNewTeamMember()
    //{
    //  using (var db = new TeamRepository())
    //  {
    //    var newTeamMember = new TeamMember();
    //    newTeamMember.Name = "Testni Developer";

    //    db.TeamMembers.Add(newTeamMember);
    //    db.SaveChanges();
    //  }
    //}
  }
}

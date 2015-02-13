using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.Entity;

using Nemesis.Domain;

namespace Nemesis.DAL.Test
{
  [TestClass]
  public class ObjectivesTests
  {
    [TestMethod]
    public void TestAddIterationObjective()
    {
        using (var repo = new GenericRepository<Objective>(new NemesisContext()))
        {
            Database.SetInitializer(new NemesisInitializer());

            var obj = new IterationObjective();
            obj.Title = "Prvi iter obj";
            obj.CreatedOn = DateTime.Now;
            obj.Description = "Testni Iteration objective";
            obj.IterationOrdNum = 1;
            obj.Priority = ObjectivePriority.HIGH;
            obj.AssignedToTeam = new Team() { Name = "TestTeam" };
//            obj.AssignedToTeam = new Team() { Name = "TestTeam", TeamLeader = new TeamMember() { Name = "TestTeam leader" } };

            repo.Insert(obj);
            repo.Save();
        }

    }
  }
}

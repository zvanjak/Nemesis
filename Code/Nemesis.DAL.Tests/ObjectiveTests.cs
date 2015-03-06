using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.Entity;

using Nemesis.Domain;
using Nemesis.DAL;

namespace Nemesis.DAL.Tests
{
	[TestClass]
	public class ObjectiveTests
	{
		[TestMethod]
		public void TestAddIterationObjective2()
		{
			using (var repo = new GenericRepository<Objective>(new NemesisContext()))
			{
				Database.SetInitializer(new NemesisInitializer());

				var obj = new Objective();
				obj.Title = "Prvi iter obj";
				obj.CreatedOn = DateTime.Now;
				obj.Description = "Testni Iteration objective";
				obj.Priority = ObjectivePriority.HIGH;
				obj.AssignedToTeam = new Team() { Name = "TestTeam" };
				//            obj.AssignedToTeam = new Team() { Name = "TestTeam", TeamLeader = new TeamMember() { Name = "TestTeam leader" } };

				repo.Insert(obj);
				repo.Save();
			}

		}
	}
}

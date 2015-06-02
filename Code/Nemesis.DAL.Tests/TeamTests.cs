using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity.Infrastructure;

using Nemesis.Domain;
using Nemesis.Domain.Assets;
using Nemesis.DAL.Configurations;

namespace Nemesis.DAL.Tests
{
	[TestClass]
	public class TeamTests
	{
		[TestMethod]
		public void Simple_add_team_and_team_leader()
		{
			NemesisContext context = new NemesisContext("NemesisContextTest");

			var zvone = new TeamMember() { Username = "Zvonimir", UserShortCode = "ZV" };
			var nemesisTeam = new Team() { Name = "Nemesis d.o.o" };
			zvone.MemberOfTheTeam = nemesisTeam;
			nemesisTeam.Members.Add(zvone);

			context.Teams.Add(nemesisTeam);
			context.TeamMembers.Add(zvone);

			context.SaveChanges();

			// potrebno je naknadno definirati Leadera
			nemesisTeam.Leader = zvone;
			context.SaveChanges();
		}

		[TestMethod]
		[ExpectedException(typeof(DbUpdateException))]
		public void Illustrate_error_when_adding_at_the_same_time()
		{
			NemesisContext context = new NemesisContext("NemesisContextTest");

			var zvone = new TeamMember() { Username = "Zvonimir" };
			var nemesisTeam = new Team() { Name = "Nemesis d.o.o", Leader = zvone };
			zvone.MemberOfTheTeam = nemesisTeam;
			nemesisTeam.Members.Add(zvone);

			context.Teams.Add(nemesisTeam);
			context.TeamMembers.Add(zvone);

			context.SaveChanges();
		}
	}
}

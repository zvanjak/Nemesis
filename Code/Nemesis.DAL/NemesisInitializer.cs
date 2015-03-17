﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

using Nemesis.Domain;

namespace Nemesis.DAL
{
	public class NemesisInitializer : DropCreateDatabaseIfModelChanges<NemesisContext>
	{
		protected override void Seed(NemesisContext context)
		{
            var director = new TeamMember() { FirstName = "Mirna", LastName = "Domančić" };
            var voditeljRazvoja = new TeamMember() { FirstName = "Samo", LastName = "Mirna" };
            var tomo = new TeamMember() { FirstName = "Tomislav", LastName = "Bradarić" };

            var test = new TeamMember() { FirstName = "Testna", LastName = "Osoba" };

            var teamLead2 = new TeamMember() { FirstName = "Mirna", LastName = "Hotfix 1.1." };

            context.TeamMembers.Add(director);
            context.TeamMembers.Add(voditeljRazvoja);
            context.TeamMembers.Add(teamLead2);

			//// NEVJEROJATNO - ako ovdje inicijaliziram TeamLeader-a dobijem exception od Entity Frameworka, a u konkretnim testovima definiranje leadera SVE RADI!!!???
			//// POPRAVLJENO
            var firma = new Team() { Name = "PalaĆinke", Leader = director };
            var odjelRazvoja = new Team() { Name = "Obiteljski pogrebni obrt Adams", Leader = voditeljRazvoja };

            firma.Members.Add(test);

            firma.Members.Add(tomo);
            odjelRazvoja.Members.Add(tomo);

            firma.Members.Add(teamLead2);
            odjelRazvoja.Members.Add(teamLead2);

            context.Teams.Add(firma);
            context.Teams.Add(odjelRazvoja);

			//Asset prog1 = new Asset() { Name = "Prva komponenta", Description = "Prva", ShortDescription = "Short", ResponsibleTeam = odjelRazvoja };
			//context.Assets.Add(prog1);

			//Feature feat1 = new Feature() { CreatedOn = DateTime.Now, BelongsToAsset = prog1, Name = "Feature 1", Description = "Detailed desc", Code = "FEAT-1" };
			//context.Features.Add(feat1);

			//Project proj1 = new Project() { Name = "Project 1" };
			//context.Projects.Add(proj1);

			//Requirement req1 = new Requirement()
			//{
			//	CreatedOn = DateTime.Now,
			//	Code = "REQ-1",
			//	BelongsToProject = proj1,
			//	Name = "Req 1",
			//	Description = "Prvi requirement"
			//};

			//context.Requirements.Add(req1);

			//// INITIALIZE ROLES
			//Array roles = Enum.GetNames(typeof(RoleType));
			//foreach (string roleName in roles)
			//{
			//	var role = new Role() { RoleName = roleName };
			//	context.Roles.Add(role);
			//}

			//Array userActions = Enum.GetNames(typeof(UserActionType));
			//foreach (string userActionName in userActions)
			//{
			//	var userAction = new UserAction() { ActionName = userActionName };
			//	context.UserActions.Add(userAction);
			//}

		    var activity = new WorkActivity()
		    {
		        Title = "Aktivnost",
		        Description = "Opis......",
                Date = DateTime.Now,
                DoneBy = tomo
		    };
		    context.Activities.Add(activity);

			context.SaveChanges();
		}
	}
}

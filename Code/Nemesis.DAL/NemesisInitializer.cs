using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

using Nemesis.Domain;
using Nemesis.Domain.Assets;

namespace Nemesis.DAL
{
	public class NemesisInitializer : DropCreateDatabaseIfModelChanges<NemesisContext>
	{
		protected override void Seed(NemesisContext context)
		{
      var director = new TeamMember() { FirstName = "Dogbert", LastName = "CEO" };
      var teamLead= new TeamMember() { FirstName = "Zvonimir", LastName = "Vanjak" };
			var tomo = new TeamMember() { FirstName = "Tomislav", LastName = "Bradarić" };
			var mirna = new TeamMember() { FirstName = "Mirna", LastName = "Domančić" };
			var mauro = new TeamMember() { FirstName = "Mauro", LastName = "Barešić" };
			var ivan = new TeamMember() { FirstName = "Ivan", LastName = "Cutvarić" };

      context.TeamMembers.Add(director);
			context.TeamMembers.Add(teamLead);
			context.TeamMembers.Add(tomo);
			context.TeamMembers.Add(mirna);

			var firma = new Team() { Name = "Dilber & Co.", Leader = director };
      
			var teamTeam = new Team() { Name = "Team&Users team", Leader = tomo, Parent = firma };
			teamTeam.Members.Add(mirna);

      var teamObjective = new Team() { Name = "Objectives team", Leader = mauro, Parent = firma };
			teamObjective.Members.Add(ivan);

      context.Teams.Add(firma);
			context.Teams.Add(teamTeam);
			context.Teams.Add(teamObjective);

			AssetAttribute attr1 = new AssetAttribute() { Name = "SW Source Code Path", Type = AssetAttributeType.String };
			AssetAttribute attr2 = new AssetAttribute() { Name = "SW Platform Type", Type = AssetAttributeType.Enum };
			AssetAttribute attr3 = new AssetAttribute() { Name = "Documentation Path", Type = AssetAttributeType.String };

			AssetType aType = new AssetType();
			aType.Name = "SW Component";
			aType.Attributes.Add(attr1);

			Asset prog1 = new Asset() { Name = "Prva komponenta", Description = "Prva" };
			prog1.Type = aType;
			
			context.Assets.Add(prog1);
			
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

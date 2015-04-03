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
      var zvone= new TeamMember() { FirstName = "Zvonimir", LastName = "Vanjak" };
			var anja = new TeamMember() { FirstName = "Anja", LastName = "Hula" };
			var sime = new TeamMember() { FirstName = "Šime", LastName = "Gverić" };
			var daria = new TeamMember() { FirstName = "Daria", LastName = "Bužić" };
			var ivana = new TeamMember() { FirstName = "Ivana", LastName = "Vanjak" };
			var mauro = new TeamMember() { FirstName = "Mauro", LastName = "Barešić" };
			var ivan = new TeamMember() { FirstName = "Ivan", LastName = "Cutvarić" };

			context.TeamMembers.Add(zvone);
			context.TeamMembers.Add(anja);
			context.TeamMembers.Add(sime);
			context.TeamMembers.Add(daria);
			context.TeamMembers.Add(ivana);
			context.TeamMembers.Add(mauro);
			context.TeamMembers.Add(ivan);

			var nemesisTeam = new Team() { Name = "Nenesis d.o.o" /*, Leader = zvone */ };
      
			var workOrdersTeam = new Team() { Name = "Work orders team" /*, Leader = ivana*/ /*, Parent = nemesisTeam */ };
			workOrdersTeam.Members.Add(ivana);
			workOrdersTeam.Members.Add(daria);

      var teamObjective = new Team() { Name = "Objectives & Activities team" /*, Leader = mauro*/ /*, Parent = nemesisTeam */ };
			teamObjective.Members.Add(mauro);
			teamObjective.Members.Add(ivan);
			teamObjective.Members.Add(anja);
			teamObjective.Members.Add(sime);

      context.Teams.Add(nemesisTeam);
			context.Teams.Add(workOrdersTeam);
			context.Teams.Add(teamObjective);

			AssetAttribute attr1 = new AssetAttribute() { Name = "SW Source Code Path", Type = AssetAttributeType.String };
			AssetAttribute attr2 = new AssetAttribute() { Name = "SW Platform Type", Type = AssetAttributeType.Enum };
			AssetAttribute attr3 = new AssetAttribute() { Name = "Documentation Path", Type = AssetAttributeType.String };

			AssetType aType = new AssetType() { Name = "SW Component" };
			aType.Attributes = new List<AssetAttribute>();
			aType.Attributes.Add(attr1);
			aType.Attributes.Add(attr2);
			aType.Attributes.Add(attr3);

			Asset prog1 = new Asset() { Name = "Prva komponenta", Description = "Prva testna komponenta koja radi nešto trubo gtx injection", PartNumber="P12-345", Type=aType };
			
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
                DoneBy = mauro
		    };
		    context.Activities.Add(activity);

			context.SaveChanges();
		}
	}
}

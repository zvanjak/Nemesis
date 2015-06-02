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
			var zvone= new TeamMember() { Username = "Zvonimir" };
			var anja = new TeamMember() { Username = "Anja" };
			var sime = new TeamMember() { Username = "Šime" };
			var daria = new TeamMember() { Username = "Daria" };
			var ivana = new TeamMember() { Username = "Ivana" };
			var mauro = new TeamMember() { Username = "Mauro" };
			var ivan = new TeamMember() { Username = "Ivan" };

			context.TeamMembers.Add(zvone);
			context.TeamMembers.Add(anja);
			context.TeamMembers.Add(sime);
			context.TeamMembers.Add(daria);
			context.TeamMembers.Add(ivana);
			context.TeamMembers.Add(mauro);
			context.TeamMembers.Add(ivan);

			var nemesisTeam = new Team() { Name = "Nemesis d.o.o" /*, Leader = zvone */ };
      
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

			//AssetAttribute attr1 = new AssetAttribute() { Name = "SW Source Code Path", Type = AssetAttributeType.String };
			//AssetAttribute attr2 = new AssetAttribute() { Name = "SW Platform Type", Type = AssetAttributeType.Enum };
			//AssetAttribute attr3 = new AssetAttribute() { Name = "Documentation Path", Type = AssetAttributeType.String };

			//AssetType aType = new AssetType() { Name = "SW Component" };
			//aType.Attributes = new List<AssetAttribute>();
			//aType.Attributes.Add(attr1);
			//aType.Attributes.Add(attr2);
			//aType.Attributes.Add(attr3);

			//Asset prog1 = new Asset() { Name = "Prva komponenta", Description = "Prva testna komponenta koja radi nešto trubo gtx injection", PartNumber = "P12-345", Type = aType, Team = teamObjective };
			//Asset prog2 = new Asset() { Name = "Druga komponenta", Description = "Druga testna komponenta koja radi nešto trubo gtx injection", PartNumber = "P3-5", Type = aType, Team = workOrdersTeam };

			//context.Assets.Add(prog1);
			//context.Assets.Add(prog2);
			
			context.SaveChanges();

			// sad idemo i pospajati teamove i leadere
			nemesisTeam.Leader = zvone;
			
			workOrdersTeam.Leader = ivana;
			workOrdersTeam.Parent = nemesisTeam;

			teamObjective.Leader = mauro;
			teamObjective.Parent = nemesisTeam;

			context.SaveChanges();
		}
	}
}

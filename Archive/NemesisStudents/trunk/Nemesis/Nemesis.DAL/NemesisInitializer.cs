using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using Nemesis.Domain;
using Nemesis.Domain.Assets;
using Nemesis.Domain.Security;

namespace Nemesis.DAL
{
  public class NemesisInitializer : DropCreateDatabaseIfModelChanges<NemesisContext>
  {
    protected override void Seed(NemesisContext context)
    {
        var director = new TeamMember() { Name = "Direktor" };
        var voditeljRazvoja = new TeamMember() { Name = "Voditelj razvoja" };
        var teamLead1 = new TeamMember() { Name = "Lead 1" };
        var teamLead2 = new TeamMember() { Name = "Lead 2" };

        context.TeamMembers.Add(director);
        context.TeamMembers.Add(voditeljRazvoja);
        context.TeamMembers.Add(teamLead1);
        context.TeamMembers.Add(teamLead2);

        // NEVJEROJATNO - ako ovdje inicijaliziram TeamLeader-a dobijem exception od Entity Frameworka, a u konkretnim testovima definiranje leadera SVE RADI!!!???
        // POPRAVLJENO
        var firma = new Team() { Name = "Naša firma d.o.o", TeamLeader = director };
        var odjelRazvoja = new Team() { Name = "Odjel razvoja", TeamLeader = voditeljRazvoja };
        var team1 = new Team() { Name = "Tim 1", TeamLeader = teamLead1 };
        var team2 = new Team() { Name = "Tim 2", TeamLeader = teamLead2 };

        context.Teams.Add(firma);
        context.Teams.Add(odjelRazvoja);
        context.Teams.Add(team1);
        context.Teams.Add(team2);

        var members = new List<TeamMember>
                      {
                        new TeamMember() {Name = "Zvone", MemberOfTheTeam = team1},
                        new TeamMember() {Name = "Ivica", MemberOfTheTeam = team1},
                        new TeamMember() {Name = "Tomislav", MemberOfTheTeam = team1},
                        new TeamMember() {Name = "Josip", MemberOfTheTeam = team2}
                        //new TeamMember() {Name = "Pero", MemberOfTheTeam = team2}
                      };
        members.ForEach(m => context.TeamMembers.Add(m));

        Asset prog1 = new Asset() { Name = "Prva komponenta", Description = "Prva", ShortDescription = "Short", ResponsibleTeam = odjelRazvoja };
        context.Assets.Add(prog1);

        Feature feat1 = new Feature() { CreatedOn = DateTime.Now, BelongsToAsset = prog1, Name = "Feature 1", Description = "Detailed desc", Code = "FEAT-1" };
        context.Features.Add(feat1);

        Project proj1 = new Project() { Name = "Project 1" };
        context.Projects.Add(proj1);

        Requirement req1 = new Requirement()
                              {
                                CreatedOn = DateTime.Now, 
                                Code = "REQ-1",
                                BelongsToProject = proj1,
                                Name = "Req 1",
                                Description = "Prvi requirement"
                              };

        context.Requirements.Add(req1);

        // INITIALIZE ROLES
        Array roles = Enum.GetNames(typeof(RoleType));
        foreach (string roleName in roles)
        {
          var role = new Role() { RoleName = roleName };
          context.Roles.Add(role);
        }

        Array userActions = Enum.GetNames(typeof(UserActionType));
        foreach (string userActionName in userActions)
        {
          var userAction = new UserAction() { ActionName = userActionName };
          context.UserActions.Add(userAction);
        }
				
        context.SaveChanges();
    }
  }
}

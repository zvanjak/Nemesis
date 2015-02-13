using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

using Nemesis.DAL.Configurations;

using Nemesis.Domain;
using Nemesis.Domain.Assets;
using Nemesis.Domain.Security;

namespace Nemesis.DAL
{
  public class NemesisContext : DbContext
  {
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserAction> UserActions { get; set; }

    public DbSet<IterationObjective> IterationObjectives { get; set; }
    public DbSet<MonthObjective> MonthObjectives { get; set; }
    public DbSet<QuartalObjective> QuartalObjectives { get; set; }

    public DbSet<Issue> Issues { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Bug> Bugs { get; set; }
    public DbSet<Requirement> Requirements { get; set; }

    public DbSet<Project> Projects { get; set; }

    public DbSet<WorkActivity> Activities { get; set; }

    public DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Configurations.Add(new TeamConfiguration());
        modelBuilder.Configurations.Add(new TeamMemberConfiguration());
        modelBuilder.Configurations.Add(new RoleConfiguration());
        modelBuilder.Configurations.Add(new UserConfiguration());

        modelBuilder.Configurations.Add(new ObjectiveConfiguration());

        modelBuilder.Configurations.Add(new WorkActivityConfiguration());

        modelBuilder.Configurations.Add(new ProjectConfiguration());
        modelBuilder.Configurations.Add(new AssetConfiguration());

        modelBuilder.Configurations.Add(new IssueConfiguration());
        modelBuilder.Configurations.Add(new FeatureConfiguration());
        modelBuilder.Configurations.Add(new BugConfiguration());
        modelBuilder.Configurations.Add(new RequirementConfiguration());

    }
  }
}

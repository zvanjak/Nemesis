using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

using TestDomainDAL;

namespace TestDomainDAL
{
	public interface IEntity
	{
		int Id { get; }
	}

	public abstract class EntityBase : IEntity
	{
		public virtual int Id { get; set; }
	}

	public class Team : EntityBase
	{
		public string Name { get; set; }

		public virtual ICollection<TeamMember> Members { get; set; }

		public virtual TeamMember Leader { get; set; }

		public Team()
		{
			Members = new List<TeamMember>();
		}
	}

	public class TeamMember : EntityBase
	{
		public virtual string FirstName { get; set; }

		public virtual string LastName { get; set; }

		public virtual Team MemberOfTheTeam { get; set; }
	}

	public class TeamConfiguration : EntityTypeConfiguration<Team>
	{
		public TeamConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name).IsRequired();

			this.HasMany(d => d.Members).WithOptional(l => l.MemberOfTheTeam); //.HasForeignKey(m => m.TeamId);
			this.HasOptional(d => d.Leader);
		}
	}

	public class TeamMemberConfiguration : EntityTypeConfiguration<TeamMember>
	{
		public TeamMemberConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.FirstName).IsRequired();
			this.Property(t => t.LastName).IsRequired();
			this.HasOptional(t => t.MemberOfTheTeam);
		}
	}
	public class TestTeamsContext : DbContext
	{
		public TestTeamsContext() : base("TestTeamsContext") { }

		public DbSet<Team> Teams { get; set; }
		public DbSet<TeamMember> TeamMembers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new TeamConfiguration());
			modelBuilder.Configurations.Add(new TeamMemberConfiguration());
		}
	}
}

namespace TestDomainDALTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestCreatingTeams()
		{
			TestTeamsContext context = new TestTeamsContext();

			var zvone = new TeamMember() { FirstName = "Zvonimir", LastName = "Vanjak" };
			var nemesisTeam = new Team() { Name = "Nemesis d.o.o" /*, Leader = zvone*/ };
			//nemesisTeam.Leader = zvone;
			zvone.MemberOfTheTeam = nemesisTeam;
			nemesisTeam.Members.Add(zvone);

			context.Teams.Add(nemesisTeam);
			context.TeamMembers.Add(zvone);

			context.SaveChanges();
		}
	}
}

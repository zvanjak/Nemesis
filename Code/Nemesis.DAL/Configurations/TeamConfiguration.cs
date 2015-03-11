using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Nemesis.Domain;

namespace Nemesis.DAL.Configurations
{
	public class TeamConfiguration : EntityTypeConfiguration<Team>
	{
		public TeamConfiguration()
		{
			this.HasKey(t => t.Id);
			// this.Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			this.Property(t => t.Name).IsRequired();
			this.HasMany(d => d.Members).WithOptional(l => l.MemberOfTheTeam);

			//this.HasMany(t => t.SubTeams).WithOptional(p => p.Parent).Map(m => m.MapKey("ParentTeamId2"));
			//this.HasOptional(d => d.Parent).WithMany(t => t.SubTeams).HasForeignKey(k => k.ParentTeamId);

			//this.HasRequired(d => d.TeamLeader);
		}
	}
}

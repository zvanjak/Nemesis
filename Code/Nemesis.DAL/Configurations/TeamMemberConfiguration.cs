using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Nemesis.Domain;

namespace Nemesis.DAL.Configurations
{
	public class TeamMemberConfiguration : EntityTypeConfiguration<TeamMember>
	{
		public TeamMemberConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Username).IsRequired();
			this.Property(t => t.UserShortCode).IsRequired();
			this.HasOptional(t => t.MemberOfTheTeam);
		}
	}
}

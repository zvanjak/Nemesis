using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nemesis.Domain;
using Nemesis.Domain.Security;

namespace Nemesis.DAL.Configurations
{
  public class UserConfiguration : EntityTypeConfiguration<User>
  {
    public UserConfiguration()
    {
      this.HasKey(t => t.Id);
      this.HasMany<Role>(r => r.UserRoles).WithMany(u => u.RoleUsers).Map(m =>
      {
        m.ToTable("UserRoles");
        m.MapLeftKey("UserId");
        m.MapRightKey("RoleId");
      });

      // this.HasRequired<TeamMember>(u => u.TeamMember).WithOptional();

      // this.HasRequired(m => m.UserRole)
      //              .WithMany(t => t.RoleUsers)
      //              .HasForeignKey(m => m.RoleId)
      //              .WillCascadeOnDelete(false);
    }
  }
}

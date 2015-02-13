using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nemesis.Domain.Security;

namespace Nemesis.DAL.Configurations
{
  public class RoleConfiguration : EntityTypeConfiguration<Role>
  {
    public RoleConfiguration()
    {
      this.HasKey(r => r.Id);
      
      this.HasMany<UserAction>(r => r.AllowedActions).WithMany(a => a.AllowedInRoles).Map(m =>
      {
        m.ToTable("UserActionsRoles");
        m.MapLeftKey("RoleId");
        m.MapRightKey("UserActionId");
      });
    }
  }
}

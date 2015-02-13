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
  public class ActionConfiguration : EntityTypeConfiguration<UserAction>
  {
    public ActionConfiguration()
    {
      this.HasKey(t => t.Id);
    }
  }
}

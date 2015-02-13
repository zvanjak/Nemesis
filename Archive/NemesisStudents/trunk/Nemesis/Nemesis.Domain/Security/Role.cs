using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Nemesis.Domain.Security
{
  public class Role : IPersistedEntity
  {
    public virtual string RoleName { get; set; }
    public virtual ICollection<UserAction> AllowedActions { get; set; }
    public virtual ICollection<User> RoleUsers { get; set; }

    public Role()
    {
      this.AllowedActions = new List<UserAction>();
      this.RoleUsers = new List<User>();
    }
  }
}

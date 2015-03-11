using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain.Security
{
	public class UserAction : EntityBase
	{
		public virtual string ActionName { get; set; }

		public virtual ICollection<Role> AllowedInRoles { get; set; }

		public override string Display { get { return ActionName; }}
		public UserAction()
		{
			this.AllowedInRoles = new List<Role>();
		}
	}
}

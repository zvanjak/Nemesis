using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain.Security
{
	public class User : EntityBase
	{
		public string Username { get; set; }
		public byte[] Password { get; set; }
		public byte[] Salt { get; set; }

		public virtual TeamMember TeamMember { get; set; }

		public override string Display { get { return Username; } }

		public virtual ICollection<Role> UserRoles { get; set; }

		public User()
		{
			this.UserRoles = new List<Role>();
		}
	}
}

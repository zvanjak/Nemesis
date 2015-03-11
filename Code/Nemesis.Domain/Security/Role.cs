using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain.Security
{
	public class Role : EntityBase
	{
		public virtual string RoleName { get; set; }
		/// <summary>
		/// Gets or sets the list of users in role.
		/// </summary>
		public ICollection<User> Users { get; set; }
		/// <summary>
		/// Gets or sets the list of rights.
		/// </summary>
		public ICollection<int> Rights { get; set; }

		public override string Display { get { return RoleName; } }

		public Role()
		{
			Users = new List<User>();
			Rights = new List<int>();
		}
	}
}

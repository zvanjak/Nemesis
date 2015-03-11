using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class Issue : EntityBase
	{
		public virtual string Code { get; set; }
		public virtual string Name { get; set; }

		public virtual DateTime CreatedOn { get; set; }

		public virtual string Description { get; set; }

		public virtual TeamMember AssignedTo { get; set; }

		public override string Display { get { return Code; } }

	}
}

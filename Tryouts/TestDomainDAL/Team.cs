using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDomainDAL
{
    public class Team : EntityBase
    {
			public string Name { get; set; }

			//public int? ParentId { get; set; }
			//public virtual Team Parent { get; set; }
			//public virtual ICollection<Team> SubTeams { get; set; }

			public virtual ICollection<TeamMember> Members { get; set; }

			public virtual TeamMember Leader { get; set; }

			public Team()
			{
				//Members = new List<TeamMember>();
				//SubTeams = new List<Team>();
			}

			public override string Display
			{
				get { return Name; }
			}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nemesis.Domain.Security;

namespace Nemesis.Domain
{
	public class Team : EntityBase
	{
		public string Name { get; set; }

		public int? ParentId { get; set; }
		public virtual Team Parent { get; set; }
		public virtual ICollection<Team> SubTeams { get; set; }

		/// <summary>
		/// Dohvaća ili postavlja vrstu tima.
		/// </summary>
		public virtual TeamTypes Type { get; set; }

		public virtual ICollection<TeamMember> Members { get; set; }

		public virtual TeamMember Leader { get; set; }

		/// <summary>
		/// Gets or sets the quality manager of the team.
		/// </summary>
		public virtual User QualityManager { get; set; }

		//////////////////////////////////////////////////////////////////////////////////////////////
		public Team()
		{
			Members = new List<TeamMember>();
            //SubTeams = new List<Team>();
		}

		public override string Display
		{
			get { return Name; }
		}

	}
}

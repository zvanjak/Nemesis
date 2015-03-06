using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
	public class Team : EntityBase
	{
		public string Name { get; set; }

		//public virtual int ParentTeamId { get; set; }
		public virtual Team Parent { get; set; }
		//public ICollection<Team> SubTeams { get; set; }

		/// <summary>
		/// Dohvaća ili postavlja vrstu tima.
		/// </summary>
		public virtual TeamType Type { get; set; }

		public virtual List<TeamMember> TeamMembers { get; set; }

		public virtual TeamMember TeamLeader { get; set; }

		//////////////////////////////////////////////////////////////////////////////////////////////
		public Team()
		{
			TeamMembers = new List<TeamMember>();
		}

		public override string Display
		{
			get { return Name; }
		}

	}
}

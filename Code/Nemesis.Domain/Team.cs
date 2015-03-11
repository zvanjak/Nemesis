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

		public virtual Team Parent { get; set; }
		public ICollection<Team> SubTeams { get; set; }

		/// <summary>
		/// Dohvaća ili postavlja vrstu tima.
		/// </summary>
		public virtual TeamType Type { get; set; }

		public virtual List<TeamMember> Members { get; set; }

		public virtual TeamMember Leader { get; set; }

		/// <summary>
		/// Gets or sets the quality manager of the team.
		/// </summary>
		public virtual User QualityManager { get; set; }

		//////////////////////////////////////////////////////////////////////////////////////////////
		public Team()
		{
			Members = new List<TeamMember>();
		}

		public override string Display
		{
			get { return Name; }
		}

	}
}

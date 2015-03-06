using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class Objective : EntityBase
	{
		public string Keloza { get; set; }

		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual int ObjectiveNumber { get; set; }

		public virtual ObjectivePriority Priority { get; set; }
		public virtual int PercentComplete { get; set; }

		public virtual TeamMember CreatedBy { get; set; }
		public virtual DateTime CreatedOn { get; set; }
		public virtual Team AssignedToTeam { get; set; }

		/// <summary>
		/// Dohvaća ili postavlja podatak je li cilj odobren.
		/// </summary>
		public virtual bool IsApproved { get; set; }
		/// <summary>
		/// Dohvaća ili postavlja procijenjeno vrijeme izvedbe cilja.
		/// </summary>
		public virtual double EstimatedTime { get; set; }
		/// <summary>
		/// Dohvaća ili postavlja vrijeme potrošeno na izvedbu cilja.
		/// </summary>
		//public virtual double ActualTime { get; set; }


		public override string Display
		{
			get { return Title; }
		}

	}
}

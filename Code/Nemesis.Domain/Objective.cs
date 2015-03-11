using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class Objective : EntityBase
	{
		/// <summary>
		/// Dohvaća ili postavlja kratki opis cilja.
		/// </summary>
		public virtual string ShortDescription { get; set; }
		/// <summary>
		/// Dohvaća ili postavlja opis cilja.
		/// </summary>
		public virtual string Description { get; set; }
		public virtual int Year { get; set; }
		public virtual int ObjectiveNumber { get; set; }

		/// <summary>
		/// Gets or sets the completion measure description of the objective. This value
		/// specifies what parameters the objective must satisfy in order to be completed.
		/// </summary>
		public virtual string CompletionMeasure { get; set; }
		/// <summary>
		/// Gets or sets the quality measure description of the objective. This value
		/// specifies what parameters the objective must satisfy in a quality control test.
		/// </summary>
		public virtual string QualityMeasure { get; set; }

		public virtual ObjectivePriority Priority { get; set; }
		public virtual int		PercentComplete { get; set; }

		public virtual TeamMember CreatedBy { get; set; }
		public virtual DateTime		CreatedOn { get; set; }

		/// <summary>
		/// Dohvaća ili postavlja tim kojem je cilj pridružen.
		/// </summary>
		public virtual Team AssignedToTeam { get; set; }
		/// <summary>
		/// Dohvaća ili postavlja listu članova tima pridruženih cilju.
		/// </summary>
		public virtual ICollection<TeamMember> AssignedToTeamMembers { get; set; }

		/// <summary>
		/// Dohvaća ili postavlja listu dnevnih aktivnosti vezanih uz cilj.
		/// </summary>
		public virtual ICollection<WorkActivity> WorkActivities { get; set; }

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
		public virtual double ActualTime { get; set; }

		#region Parent/child

		/// <summary>
		/// Gets or sets the parent objective.
		/// </summary>
		public virtual Objective Parent { get; set; }
		/// <summary>
		/// Gets or sets the list of child objectives
		/// </summary>
		public virtual ICollection<Objective> Objectives { get; set; }

		#endregion

		public override string Display
		{
			get { return ShortDescription; }
		}

	}
}

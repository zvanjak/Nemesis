using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class WorkActivity : EntityBase
	{
        public virtual string Title { get; set; }

		public virtual string Description { get; set; }

		public virtual DateTime Date { get; set; }   // datum kad je aktivnost odrađena

		public virtual double EstimatedDuration { get; set; }

		public virtual double ActualDuration { get; set; }

		public virtual double OvertimeDuration { get; set; }

		public virtual TeamMember DoneBy { get; set; }

		public virtual Objective RealizedForObjective { get; set; }

        //public virtual WorkActivityGrade Grade { get; set; }

		public override string Display
		{
			get { return Title + " (" + DoneBy.Display + ")"; }
		}
	}
}

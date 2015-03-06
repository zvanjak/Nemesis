using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class WorkActivity : EntityBase
	{
		public virtual string Description { get; set; }

		public virtual DateTime Date { get; set; }   // datum kad je aktivnost odrađena

		public virtual int EstimatedDuration { get; set; }
		public virtual int ActualDuration { get; set; }
		public virtual int OvertimeDuration { get; set; }

		public virtual TeamMember DoneBy { get; set; }

		//public virtual Objective RealizedForObjective { get; set; }

		public override string Display
		{
			get { return Description; }
		}
	}
}

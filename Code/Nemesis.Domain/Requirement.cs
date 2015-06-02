using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class Requirement : Issue
	{
		public override string Display
		{
			get { return string.Format("{0} - {1}", Code, Name); }
		}

		public virtual Project BelongsToProject { get; set; }
	}
}

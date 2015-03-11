using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
    public class Project : EntityBase
    {
			public virtual string Name { get; set; }
			public virtual string ShortDescription { get; set; }
			public virtual string Description { get; set; }

			public virtual List<Requirement> ReqsList { get; set; }

			public override string Display
			{
				get { return Name; }
			}

			public Project()
			{
				ReqsList = new List<Requirement>();
			}

    }
}

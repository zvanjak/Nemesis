using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class Client : EntityBase
	{
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public override string Display
        {
            get { return Description; }
        }
    }
}

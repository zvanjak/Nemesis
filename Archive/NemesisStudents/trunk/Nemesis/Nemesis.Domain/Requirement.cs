using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
    public class Requirement : Issue
    {
        public string Display
        {
          get { return string.Format("{0} - {1}", Code, Name); }
        }

        public virtual Project BelongsToProject { get; set; }

        //public virtual Requirement Parent { get; set; }
    }
}

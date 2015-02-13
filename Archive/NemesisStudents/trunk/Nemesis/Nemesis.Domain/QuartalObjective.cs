using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
    public class QuartalObjective : Objective
    {
        public virtual int Quartal {get; set;}
        public virtual int Year { get; set; }

    }
}

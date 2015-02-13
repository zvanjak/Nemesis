using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
  public class MonthObjective : Objective
  {
      public virtual int Year {get; set;}
      public virtual int Month { get; set; }
  }
}

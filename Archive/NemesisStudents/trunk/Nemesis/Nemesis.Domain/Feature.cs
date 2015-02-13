using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nemesis.Domain.Assets;

namespace Nemesis.Domain
{
  public class Feature : Issue
  {
    public string Display
    {
      get { return string.Format("{0} - {1}", Code, Name); }
    }

    public virtual Asset BelongsToAsset { get; set; }
    //public virtual Feature Parent { get; set; }

  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
    public class Project : IPersistedEntity
    {
        public virtual string Name { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Description { get; set; }

        public virtual List<Requirement> ReqsList { get; set; }

        public Project()
        {
            ReqsList = new List<Requirement>();
        }
    }
}

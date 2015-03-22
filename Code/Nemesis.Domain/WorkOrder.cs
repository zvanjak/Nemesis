using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
    public class WorkOrder : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EstimatedEndDate { get; set; }

        public virtual Client Client { get; set; }

        public virtual byte[] Document { get; set; }

        public virtual ICollection<Team> AssignedToTeams { get; set; }

        public override string Display
        {
            get { return Name; }
        }
    }
}

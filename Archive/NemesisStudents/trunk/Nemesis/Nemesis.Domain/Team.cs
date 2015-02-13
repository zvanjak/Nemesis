using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Nemesis.Domain
{
    public class Team : IPersistedEntity
    {
        public string   Name { get; set; }

        //public virtual int ParentTeamId { get; set; }
        //public virtual Team Parent { get; set; }
        //public ICollection<Team> SubTeams { get; set; }

        /// <summary>
        /// Dohvaća ili postavlja vrstu tima.
        /// </summary>
        public virtual TeamType Type { get; set; }

        public virtual List<TeamMember> TeamMembers { get; set; }

        public virtual TeamMember TeamLeader { get; set;  }

        //////////////////////////////////////////////////////////////////////////////////////////////
        public Team()
        {
          TeamMembers = new List<TeamMember>();
        }
  }
}

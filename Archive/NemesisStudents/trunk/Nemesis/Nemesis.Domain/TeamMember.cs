using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Nemesis.Domain
{
    public  class TeamMember : IPersistedEntity
    {

      public virtual string FirstName { get; set; }
      
      public virtual string LastName { get; set; }

      public virtual string Name { get; set; }

      public virtual Team MemberOfTheTeam { get; set; }
      //{
      //  get { return _memberOfTheTeam; }
      //  set
      //  {
      //    if (value != null)     // ako doista postavljamo membera kao člana nekog tima
      //    {
      //      _memberOfTheTeam = value;
      //      value.TeamMembers.Add(this); // onda ćemo ga dodati i u odgovarajuću kolekciju članova tog tima
      //    }
      //  }
      //}

      public TeamMember() { }

      public TeamMember(string inName)
      {
        Name = inName;
      }
    }
}

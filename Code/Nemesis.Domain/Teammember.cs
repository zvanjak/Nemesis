using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
	public class TeamMember : EntityBase
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

		public override string Display
		{
			get { return Name; }
		}

		public TeamMember() { }

		public TeamMember(string inName)
		{
			Name = inName;
		}

	}
}

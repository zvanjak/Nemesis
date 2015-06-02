using System.Collections.Generic;
using System.Linq;
using Nemesis.DAL;
using Nemesis.Domain;

namespace Nemesis.Services
{
    public class TeamMemberService
    {
        public static ICollection<TeamMember> GetTeamMembers(int[] ids)
        {
            using (var repository = new GenericRepository<TeamMember>(new NemesisContext()))
            {
                return ids == null ? null : ids.Select(id => repository.GetByID(id)).ToList();
            }
        }

        public static IEnumerable<TeamMember> GetTeamMembers()
        {
            using (var repository = new GenericRepository<TeamMember>(new NemesisContext()))
            {
                return repository.Get();
            }
        }	

		public static void AddNewTeamMember(TeamMember inTeamMember)
		{
            using (var repository = new GenericRepository<TeamMember>(new NemesisContext()))
			{
				repository.Insert(inTeamMember);
				repository.Save();
			}
		}
    }
}
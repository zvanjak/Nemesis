using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.DAL;
using Nemesis.Domain;

namespace Nemesis.Services
{
    public class ObjectiveService
    {
        public static ICollection<Objective> GetObjectives<T>() where T : Objective
        {
            using (var repository = new GenericRepository<T>(GetNemesisContext()))
            {
                return repository.Get().ToList<Objective>();
            }
        }

        public static ICollection<Objective> GetObjectivesFor<T>(string date) where T : Objective
        {
            GetNemesisContext();
            using (var repository = new GenericRepository<T>(GetNemesisContext()))
            {
                return
                    repository.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(date)).ToList<Objective>();
            }
        }

        public static Objective GetObjective(int id)
        {
            using (var repository = new GenericRepository<Objective>(GetNemesisContext()))
            {
                return repository.GetByID(id);
            }
        }

        private static NemesisContext GetNemesisContext()
        {
            var nemesisContext = new NemesisContext();
            nemesisContext.Configuration.LazyLoadingEnabled = false;
            return nemesisContext;
        }

        public static void Create(Objective objective, int parentId, int[] teamMemberIds) 
        {
            using (var context = new NemesisContext())
            {
                using (var membersRepo = new GenericRepository<TeamMember>(context))
                using (var objectiveRepository = new GenericRepository<Objective>(context))
                {
                    objective.Parent = objectiveRepository.GetByID(parentId);
                    var members = teamMemberIds.Select(id => membersRepo.GetByID(id)).ToList();
                    objective.AssignedToTeamMembers = members;

                    objectiveRepository.Insert(objective);
                    objectiveRepository.Save();
                }
            }
        }
    }
}
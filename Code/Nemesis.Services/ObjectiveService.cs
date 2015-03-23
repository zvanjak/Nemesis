using System.Collections.Generic;
using System.Linq;
using Nemesis.DAL;
using Nemesis.Domain;
using System.Linq.Expressions;
using System;

namespace Nemesis.Services
{
    public class ObjectiveService
    {
        public static ICollection<Objective> GetObjectives<T>(Expression<Func<T, bool>> filter = null) where T : Objective
        {
            using (var repository = new GenericRepository<T>(new NemesisContext()))
            {
                return repository.Get(filter).ToList<Objective>();
            }
        }

        public static ICollection<Objective> GetObjectivesFor<T>(string date) where T : Objective
        {
            using (var repository = new GenericRepository<T>(new NemesisContext()))
            {
                return
                    repository.Get().Where(x => x.CreatedOn.ToString("yyyy-MM-dd").Equals(date)).ToList<Objective>();
            }
        }

        public static Objective GetObjective(int id)
        {
            using (var repository = new GenericRepository<Objective>(new NemesisContext()))
            {
                return repository.GetByID(id);
            }
        }

        public static void Save(Objective objective)
        {
            using (var objectiveRepositroy = new GenericRepository<Objective>(new NemesisContext()))
            {
                objectiveRepositroy.Insert(objective);
                objectiveRepositroy.Save();
            }
        }
    }
}
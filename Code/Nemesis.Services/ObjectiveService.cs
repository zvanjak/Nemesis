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
            using (NemesisContext nc = new NemesisContext())
            {
                using (var repository = new GenericRepository<T>(nc))
                {
                    return repository.Get().ToList<Objective>();
                }
            }
        }
    }
}
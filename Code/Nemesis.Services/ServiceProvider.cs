using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nemesis.DAL;
using Nemesis.Domain;

namespace Nemesis.Services
{
    public class ServiceProvider
    {
        public static IActivityService ActiviyService(GenericRepository<WorkActivity> repo)
        {
            return new ActivityService(repo);
        }
    }
}

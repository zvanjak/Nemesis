using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nemesis.Domain;

namespace Nemesis.DAL
{
    public class ActivityRepository : GenericRepository<WorkActivity>
    {
        public ActivityRepository(NemesisContext inContext) : base(inContext) { }
    }
}

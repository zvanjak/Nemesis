using Nemesis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.DAL.Repositories
{
    public class WorkOrderRepository : GenericRepository<WorkOrder>
	{
        public WorkOrderRepository(NemesisContext inContext)
            : base(inContext)
		{ }
	}
}

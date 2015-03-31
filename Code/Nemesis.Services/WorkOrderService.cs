using Nemesis.DAL;
using Nemesis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Services
{
    public class WorkOrderService
    {
        public static ICollection<WorkOrder> GetWorkOrders()
        {
            using (var repository = new GenericRepository<WorkOrder>(GetNemesisContext()))
            {
                return repository.Get().ToList<WorkOrder>();
            }
        }

        private static NemesisContext GetNemesisContext()
        {
            var nemesisContext = new NemesisContext();
            nemesisContext.Configuration.LazyLoadingEnabled = false;
            return nemesisContext;
        }
    }
}

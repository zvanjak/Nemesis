using Nemesis.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.DAL.Configurations
{
    public class WorkOrderConfiguration : EntityTypeConfiguration<WorkOrder>
    {
        public WorkOrderConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
			this.Property(t => t.Description);
            this.Property(t => t.StartDate);
            this.Property(t => t.EstimatedEndDate);
            this.HasRequired(t => t.Client);

		}
    }
}

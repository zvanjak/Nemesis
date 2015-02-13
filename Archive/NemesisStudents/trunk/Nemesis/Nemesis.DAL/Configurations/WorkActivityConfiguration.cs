using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nemesis.Domain;

namespace Nemesis.DAL.Configurations
{
    public class WorkActivityConfiguration : EntityTypeConfiguration<WorkActivity>
    {
        public WorkActivityConfiguration()
        {
            this.HasKey(t => t.Id);
            //this.HasMany(t => t.FeatureList).WithRequired(d => d.BelongsToAsset);
            //this.HasRequired(t => t.ResponsibleTeam);
        }
    }
}

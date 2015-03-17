using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;

using Nemesis.Domain;

namespace Nemesis.DAL.Configurations
{
	public class WorkActivityConfiguration : EntityTypeConfiguration<WorkActivity>
	{
		public WorkActivityConfiguration()
		{
			this.HasKey(t => t.Id);
		    this.Property(t => t.Title);
		    this.Property(t => t.Description);
		    this.Property(t => t.EstimatedDuration);
		    this.Property(t => t.OvertimeDuration);
		    this.Property(t => t.ActualDuration);
		    this.HasOptional(t => t.DoneBy);
		    //this.HasMany(t => t.FeatureList).WithRequired(d => d.BelongsToAsset);
		    //this.HasRequired(t => t.ResponsibleTeam);
		}
	}
}

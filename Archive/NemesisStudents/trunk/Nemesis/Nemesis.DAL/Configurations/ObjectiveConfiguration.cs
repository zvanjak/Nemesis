using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Nemesis.Domain;

namespace Nemesis.DAL.Configurations
{
  public class ObjectiveConfiguration : EntityTypeConfiguration<Objective>
  {
    public ObjectiveConfiguration()
    {
        this.HasKey(t => t.Id);
        this.Property(t => t.Title).IsRequired();

        this.HasRequired(t => t.AssignedToTeam);
        this.HasOptional(t => t.CreatedBy);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Nemesis.Domain;

namespace Nemesis.DAL.Configurations
{
    public class IssueConfiguration : EntityTypeConfiguration<Issue>
    {
        public IssueConfiguration()
        {
            this.HasKey(t => t.Id);
        }
    }

    public class FeatureConfiguration : EntityTypeConfiguration<Feature>
    {
        public FeatureConfiguration()
        {
            this.HasKey(t => t.Id);
        }
    }

    public class BugConfiguration : EntityTypeConfiguration<Bug>
    {
        public BugConfiguration()
        {
            this.HasKey(t => t.Id);
        }
    }

    public class RequirementConfiguration : EntityTypeConfiguration<Requirement>
    {
        public RequirementConfiguration()
        {
            this.HasKey(t => t.Id);
        }
    }
}

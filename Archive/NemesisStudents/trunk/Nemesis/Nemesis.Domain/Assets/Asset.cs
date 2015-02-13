using System.Collections.Generic;

namespace Nemesis.Domain.Assets
{
    public class Asset : IPersistedEntity
    {
        public virtual string Name { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Description { get; set; }

        //public virtual Asset Parent { get; set; }
        
        public virtual Team ResponsibleTeam { get; set; }

        public virtual List<Feature> FeatureList { get; set; }

        //public virtual ICollection<Asset> Children { get; set; }

        /*
            public virtual AssetType Type { get; set; }
        
            public virtual ICollection<AssetVersion> ReleasedVersions { get; set; }
            public virtual ICollection<AssetPlannedRelease> PlannedReleases { get; set; }
            public virtual ICollection<AssetAttributeValue> AttributeDefaultValues { get; set; }
            public virtual ICollection<Feature> Features { get; set; }
        */

        public Asset()
        {
            FeatureList = new List<Feature>();
        }
  }
}

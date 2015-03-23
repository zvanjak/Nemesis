using System.Collections.Generic;
using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    public class AssetType : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual ICollection<AssetAttribute> Attributes { get; set; }

				public AssetType()
				{
					Attributes = new List<AssetAttribute>();
				}

        #region Overrides of EntityBase

        public override string Display
        {
            get { return Name; }
        }

        #endregion
    }
}

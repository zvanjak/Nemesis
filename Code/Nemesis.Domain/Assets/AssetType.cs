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
				}

				public virtual void AddAttribute(AssetAttribute inAttr)
				{
					if (Attributes == null)
						Attributes = new List<AssetAttribute>();

					Attributes.Add(inAttr);
				}
        #region Overrides of EntityBase

        public override string Display
        {
            get { return Name; }
        }

        #endregion
    }
}

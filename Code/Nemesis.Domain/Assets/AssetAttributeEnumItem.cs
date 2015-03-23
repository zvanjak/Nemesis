using System;

namespace Nemesis.Domain.Assets
{
    public class AssetAttributeEnumItem : EntityBase
    {
        public string Name { get; set; }
        public AssetAttribute Attribute { get; set; }

        public AssetAttributeEnumItem()
        {
            Name = string.Empty;
            Attribute = null;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
                return Id == ((AssetAttributeEnumItem)obj).Id;

            return false;
        }
        public override string ToString()
        {
            return Name;
        }

				public override string Display
				{
					get { return this.ToString(); }
				}
    }
}

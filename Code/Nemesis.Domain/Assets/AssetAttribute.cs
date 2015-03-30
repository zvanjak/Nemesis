using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    public class AssetAttribute : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual AssetAttributeType Type { get; set; }
        public virtual ICollection<AssetAttributeEnumItem> EnumValues { get; set; }

        public static AssetAttribute Create()
        {
            var attribute = new AssetAttribute {Type = AssetAttributeType.String};
            return attribute;
        }

			public AssetAttribute()
				{
					//EnumValues = new List<AssetAttributeEnumItem>();
				}
        #region Public methods

        private AssetAttributeValue CreateNewAssetAttributeValue()
        {
            AssetAttributeValue value;
            switch (Type)
            {
                case AssetAttributeType.String:
                case AssetAttributeType.Link:
                    value = new AssetAttributeValueText();
                    break;
                case AssetAttributeType.Value:
                    value = new AssetAttributeValueNumber();
                    break;
                case AssetAttributeType.Document:
                    value = new AssetAttributeValueDocument();
                    break;
                case AssetAttributeType.Enum:
                    value = new AssetAttributeValueEnum();
                    break;
                default:
                    throw new InvalidAttributeTypeException();
            }
            value.Attribute = this;

            return value;
        }
        public virtual AssetAttributeValue GetNewAttributeValue(Version version)
        {
            var value = CreateNewAssetAttributeValue();

            //value.AssetVersion = version;

            return value;
        }
        public virtual AssetAttributeValue GetNewAttributeValue(Asset asset)
        {
            var value = CreateNewAssetAttributeValue();

            value.Asset = asset;

            return value;
        }
				public virtual void AddEnumValue(string inEnumValue)
				{
					if (EnumValues == null)
						EnumValues = new List<AssetAttributeEnumItem>();

					EnumValues.Add(new AssetAttributeEnumItem() { Name = inEnumValue });
				}

        public virtual void ParseEnumValues(string enumValues)
        {
            var values = enumValues.Split(',');

            if (EnumValues == null)
                EnumValues = new List<AssetAttributeEnumItem>();

            // Brisanje itema koji više nisu u listi.
            var deleteList = EnumValues.Where(enumItem => values.Count(v => enumItem.Name == v) == 0).ToList();
            foreach (var enumItem in deleteList)
            {
                EnumValues.Remove(enumItem);
            }

            // Dodavanje novih itema.
            foreach (var value in values)
            {
                var valueCopy = value;
                if (EnumValues.Count(ev => ev.Name == valueCopy) == 0)
                {
                    var enumValue = new AssetAttributeEnumItem { Name = value, Attribute = this };

                    EnumValues.Add(enumValue);
                }
            }
        }
        public virtual string GetEnumValues()
        {
            var builder = new StringBuilder();
            foreach (var enumItem in EnumValues)
            {
                builder.Append(enumItem.Name + ",");
            }
            // Obriši zadnji zarez
            if (builder.Length > 0)
                builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        #endregion

        #region Overrides of EntityBase

        public override string Display
        {
            get { return Name; }
        }

        #endregion
    }
}

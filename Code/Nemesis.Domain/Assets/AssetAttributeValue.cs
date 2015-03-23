using System;
using System.Globalization;
using System.IO;

namespace Nemesis.Domain.Assets
{
    public class AssetAttributeValue : EntityBase
    {
        public AssetAttribute Attribute { get; set; }
        public Asset Asset { get; set; }
        //public Version AssetVersion { get; set; }
        public object Value { get; set; }

        protected AssetAttributeValue()
        {
        }

        public virtual AssetAttributeValue Copy()
        {
            return null;
        }

				public override string Display
				{
					get { return this.ToString(); }
				}
    }

    public class AssetAttributeValueText : AssetAttributeValue
    {
        public string Text
        {
            get { return Value.ToString(); }
            set { Value = value; }
        }

        public AssetAttributeValueText()
        {
            Text = string.Empty;
        }

        public override AssetAttributeValue Copy()
        {
            var value = new AssetAttributeValueText
            {
                Attribute = Attribute,
                Asset = Asset,
                //AssetVersion = AssetVersion,
                Text = Text
            };

            return value;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class AssetAttributeValueNumber : AssetAttributeValue
    {
        public double Number
        {
            get { return (double)Value; }
            set { Value = value; }
        }

        public AssetAttributeValueNumber()
        {
            Number = 0.0;
        }

        public override AssetAttributeValue Copy()
        {
            var value = new AssetAttributeValueNumber
            {
                Attribute = Attribute,
                Asset = Asset,
                //AssetVersion = AssetVersion,
                Number = Number
            };

            return value;
        }

        public override string ToString()
        {
            return Number.ToString(CultureInfo.InvariantCulture);
        }
    }

    public class AssetAttributeValueDocument : AssetAttributeValue
    {
        public Stream Document
        {
            get
            {
                return DocumentBytes != null ? new MemoryStream(DocumentBytes) : null;
            }
            set
            {
                if (value != null)
                {
                    DocumentBytes = null;
                    DocumentBytes = new byte[(int)value.Length];
                    value.Read(DocumentBytes, 0, (int)value.Length);

                    Value = new MemoryStream(DocumentBytes);
                }
            }
        }
        public byte[] DocumentBytes { get; set; }
        public string DocumentName { get; set; }

        public AssetAttributeValueDocument()
        {
            DocumentBytes = null;
            DocumentName = string.Empty;
        }

        public override AssetAttributeValue Copy()
        {
            var value = new AssetAttributeValueDocument
            {
                Attribute = Attribute,
                Asset = Asset,
                //AssetVersion = AssetVersion,
                DocumentName = DocumentName,
                Document = Document
            };

            return value;
        }

        public override string ToString()
        {
            return DocumentName;
        }
    }

    public class AssetAttributeValueEnum : AssetAttributeValue
    {
        public AssetAttributeEnumItem EnumValue
        {
            get { return Value as AssetAttributeEnumItem; }
            set { Value = value; }
        }

        public AssetAttributeValueEnum()
        {
            EnumValue = null;
        }

        public override AssetAttributeValue Copy()
        {
            var value = new AssetAttributeValueEnum
            {
                Attribute = Attribute,
                Asset = Asset,
                //AssetVersion = AssetVersion,
                EnumValue = EnumValue
            };

            return value;
        }

        public override string ToString()
        {
            return EnumValue.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;

using Nemesis.Domain.Assets;

namespace Nemesis.DAL.Configurations
{
	public class AssetConfiguration : EntityTypeConfiguration<Asset>
	{
			public AssetConfiguration() 
      {
          this.HasKey(t => t.Id);
          this.Property(t => t.Description);
          this.Property(t => t.Name);
					this.HasRequired(t => t.Team);
					this.HasRequired(t => t.Type);
      }
	}

	public class AssetTypeConfiguration : EntityTypeConfiguration<AssetType>
	{
		public AssetTypeConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
			this.HasMany(t => t.Attributes);
		}
	}

	public class AssetAttributeConfiguration : EntityTypeConfiguration<AssetAttribute>
	{
		public AssetAttributeConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
			this.Property(t => t.Type);
			this.HasOptional(t => t.EnumValues);
		}
	}

	public class AssetAttributeEnumItemConfiguration : EntityTypeConfiguration<AssetAttributeEnumItem>
	{
		public AssetAttributeEnumItemConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
			this.HasRequired(t => t.Attribute);
		}
	}


}

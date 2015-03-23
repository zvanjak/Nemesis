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
      }
	}

	public class AssetTypeConfiguration : EntityTypeConfiguration<AssetType>
	{
		public AssetTypeConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
		}
	}

	public class AssetAttributeConfiguration : EntityTypeConfiguration<AssetAttribute>
	{
		public AssetAttributeConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
		}
	}

	public class AssetAttributeEnumItemConfiguration : EntityTypeConfiguration<AssetAttributeEnumItem>
	{
		public AssetAttributeEnumItemConfiguration()
		{
			this.HasKey(t => t.Id);
			this.Property(t => t.Name);
		}
	}


}

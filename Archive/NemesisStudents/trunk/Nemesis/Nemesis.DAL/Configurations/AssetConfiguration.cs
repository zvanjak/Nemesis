﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.ModelConfiguration;
using Nemesis.Domain;
using Nemesis.Domain.Assets;

namespace Nemesis.DAL.Configurations
{
  public class AssetConfiguration : EntityTypeConfiguration<Asset>
  {
    public AssetConfiguration()
    {
        this.HasKey(t => t.Id);
        this.HasMany(t => t.FeatureList).WithRequired(d => d.BelongsToAsset);
        this.HasRequired(t => t.ResponsibleTeam);
    }
  }
}

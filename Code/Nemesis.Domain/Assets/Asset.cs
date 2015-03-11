using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Domain;
using Nemesis.Domain.Security;

namespace Nemesis.Domain.Assets
{
    public class Asset : EntityBase
    {
        #region General properties

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual AssetType Type { get; set; }
        public virtual Team Team { get; set; }
        public virtual AssetAssignments Assignments { get; set; }

        public virtual ICollection<AssetAttributeValue> DefaultTypeValues { get; set; }

        #endregion

        #region Versions

        public virtual ICollection<PlannedVersion> PlannedVersions { get; set; }
        
        public virtual ICollection<Version> Versions { get; set; }
        public virtual DateTime? LatestVersionDate
        {
            get
            {
                if (Versions != null && Versions.Any())
                    return Versions.Max(item => item.ReleaseDate);
                return null;
            }
        }

        #endregion

        #region Parent/child

        public virtual Asset Parent { get; set; }
        public virtual ICollection<Asset> Children { get; set; }

        #endregion

        public static Asset Create()
        {
            var asset = new Asset
                            {
                                DefaultTypeValues = new List<AssetAttributeValue>(),
                                PlannedVersions = new List<PlannedVersion>(),
                                Versions = new List<Version>(),
                                Children = new List<Asset>(),
                                Assignments = new AssetAssignments()
                            };

            return asset;
        }

        #region Public methods

        public virtual bool IsUsersAsset(User user)
        {
            if (Assignments.IsUsersAsset(user))
                return true;

            return Children.Any(item => item.IsUsersAsset(user));
        }
        public virtual IEnumerable<Asset> GetAllChildren(bool includeSelf)
        {
            if (includeSelf)
                return GetAllChildren(this);

            var list = new List<Asset>();
            foreach (var child in Children)
            {
                list.AddRange(GetAllChildren(child));
            }
            return list;
        }
        public virtual void FillAttributeValuesList()
        {
            DefaultTypeValues = new List<AssetAttributeValue>();
            foreach (var attribute in Type.Attributes)
            {
                DefaultTypeValues.Add(attribute.GetNewAttributeValue(this));
            }
        }

        #endregion

        #region Helper methods

        private IEnumerable<Asset> GetAllChildren(Asset asset)
        {
            var list = new List<Asset> { asset };

            foreach (var child in asset.Children)
            {
                var childList = GetAllChildren(child);
                list.AddRange(childList);
            }

            return list;
        }

        #endregion

        #region Overrides of EntityBase

        public override string Display
        {
            get
            {
                return Name;
            }
        }

        #endregion
    }
}

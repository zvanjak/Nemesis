using System;
using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    /// <summary>
    /// Data repository interface for entity <b>AssetAttribute</b>.
    /// </summary>
    public interface IAssetAttributeRepository
    {
        /// <summary>
        /// Returns the value of an asset attribute with the specified ID.
        /// </summary>
        AssetAttributeValue GetAttributeValue(Guid id);
        /// <summary>
        /// Returns an asset attribute enum value with the specified ID.
        /// </summary>
        AssetAttributeEnumItem GetEnumItem(Guid id);
    }
}

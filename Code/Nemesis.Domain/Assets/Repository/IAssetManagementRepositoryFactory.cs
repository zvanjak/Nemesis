using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    /// <summary>
    /// Base interface for <b>Asset management module</b> data repository factory.
    /// </summary>
    public interface IAssetManagementRepositoryFactory
    {
        /// <summary>
        /// Return an instance of data repository for domain entity <b>Asset</b>.
        /// </summary>
        IAssetRepository CreateAssetRepository();
        /// <summary>
        /// Return an instance of data repository for domain entity <b>AssetType</b>.
        /// </summary>
        IAssetTypeRepository CreateAssetTypeRepository();
        /// <summary>
        /// Return an instance of data repository for domain entity <b>AssetAttribute</b>.
        /// </summary>
        IAssetAttributeRepository CreateAssetAttributeRepository();

        /// <summary>
        /// Return an instance of data repository for domain entity <b>PlannedVersion</b>.
        /// </summary>
        IPlannedVersionRepository CreatePlannedVersionRepository();

        /// <summary>
        /// Return an instance of data repository for domain entity <b>Version</b>.
        /// </summary>
        IVersionRepository CreateVersionRepository();
    }
}

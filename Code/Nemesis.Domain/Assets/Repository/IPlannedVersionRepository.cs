using System;
using System.Collections.Generic;
using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    /// <summary>
    /// Data repository interface for entity <b>PlannedVersion</b>.
    /// </summary>
    public interface IPlannedVersionRepository
    {
        /// <summary>
        /// Returns a list of all unreleased planned versions.
        /// </summary>
        ICollection<PlannedVersion> GetAllUnreleased();

        /// <summary>
        /// Returns a list of revision planned versions with the specified base version.
        /// </summary>
        ICollection<PlannedVersion> GetAll(Guid baseVersion);
    }
}

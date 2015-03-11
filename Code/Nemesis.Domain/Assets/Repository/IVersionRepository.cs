using System;
using System.Collections.Generic;
using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    /// <summary>
    /// Data repository interface for entity <b>Version</b>.
    /// </summary>
    public interface IVersionRepository 
    {
        /// <summary>
        /// Returns a list of revision versions with the specified base version.
        /// </summary>
        ICollection<Version> GetAll(Guid baseVersion);
    }
}

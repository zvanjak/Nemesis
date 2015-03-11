using System;
using Nemesis.Domain;

namespace Nemesis.Domain.Assets
{
    public class PlannedVersion : EntityBase
    {
        public virtual string VersionNumber { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsReleased { get; set; }
        public virtual DateTime? ReleaseDate { get; set; }

        public virtual string Version1
        {
            get
            {
                var components = VersionNumber.Split('.');
                return components.Length > 0 ? components[0] : "0";
            }
        }
        public virtual string Version2
        {
            get
            {
                var components = VersionNumber.Split('.');
                return components.Length > 1 ? components[1] : "0";
            }
        }
        public virtual string Version3
        {
            get
            {
                var components = VersionNumber.Split('.');
                return components.Length > 2 ? components[2] : "0";
            }
        }
        public virtual string Version4
        {
            get
            {
                var components = VersionNumber.Split('.');
                return components.Length > 3 ? components[3] : "0";
            }
        }

        public virtual Guid? BaseVersion { get; set; }
        public virtual bool IsRevision { get; set; }

        public static PlannedVersion Create()
        {
            var plannedVersion = new PlannedVersion {Date = DateTime.Today};
            return plannedVersion;
        }

        public virtual void Release()
        {
            if (!IsReleased)
            {
                IsReleased = true;
                ReleaseDate = DateTime.Today;
            }
        }

        #region Overrides of EntityBase

        public override string Display
        {
            get { return VersionNumber; }
        }

        #endregion
    }
}

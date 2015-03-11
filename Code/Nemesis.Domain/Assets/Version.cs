using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.Domain;
using Nemesis.Domain.Security;

namespace Nemesis.Domain.Assets
{
    public class Version : EntityBase
    {
        #region Public properties

        public virtual Asset Asset { get; set; }
        public virtual PlannedVersion PlannedVersion { get; set; }
        public virtual string VersionNumber { get; set; }
        public virtual DateTime ReleaseDate { get; set; }
        public virtual string ReleaseLocations { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<AssetAttributeValue> Attributes { get; set; }

        public virtual Guid? BaseVersion { get; set; }
        public virtual bool IsRevision { get; set; }

        #endregion

        #region Approval

        public virtual ICollection<User> ApprovalList { get; set; }
        public virtual DateTime? ApprovalDate { get; set; }
        public virtual bool IsApprovedByQualityManager { get; set; }
        public virtual string ApprovedByQualityManager { get; set; }
        public virtual bool IsApproved
        {
            get
            {
                if (Asset != null)
                {
                    var responsibles =
                        Asset.Assignments.Responsible.All(responsibleMember => ApprovalList.Contains(responsibleMember));

                    var qualityManager = Asset.Team.QualityManager;
                    IsApprovedByQualityManager = qualityManager == null || IsApprovedByQualityManager;

                    return responsibles && IsApprovedByQualityManager;
                }

                return false;
            }
        }

        public virtual void Approve(User user)
        {
            // Potvrda od strane Quality Managera
            if (user.Equals(Asset.Team.QualityManager))
            {
                if (!IsApprovedByQualityManager)
                {
                    IsApprovedByQualityManager = true;
                    ApprovedByQualityManager = user.Username;
                }
            }
            else if (!ApprovalList.Contains(user))
                ApprovalList.Add(user);

            if (IsApproved)
                ApprovalDate = DateTime.Today;
        }
        public virtual bool CanApprove(User user)
        {
            if (IsApproved)
                return false;

            var qualityManager = Asset.Team.QualityManager;
            // Ako je member koji pokušava potvrditi Quality Manager
            // on može potvrditi bilo koji Asset svog tima koji
            // već nije potvrdio
            if (user.Equals(qualityManager))
            {
                return !IsApprovedByQualityManager;
            }

            // Ostali memberi mogu potvrditi Asset ako su navedeni
            // kao Responsible i ako je Quality Manager već potvrdio
            if (qualityManager != null)
            {
                return Asset.Assignments.Responsible.Contains(user) && !ApprovalList.Contains(user) &&
                       IsApprovedByQualityManager;
            }

            return Asset.Assignments.Responsible.Contains(user) && !ApprovalList.Contains(user);
        }
        public virtual bool AlreadyApproved(User user)
        {
            if (user.Equals(Asset.Team.QualityManager))
                return IsApprovedByQualityManager;

            return ApprovalList.Contains(user);
        }
        public virtual bool AwaitingApproval(User user)
        {
            // Ako je korisnik Quality Manager, vrati true ako
            // nije potvrdio release
            if (user.Equals(Asset.Team.QualityManager))
                return !IsApprovedByQualityManager;

            // Ako je korisnik naveden kao Responsible, vrati true
            // ako je Quality Manager potvrdio cilj, a korisnik još nije
            if (Asset.Assignments.Responsible.Contains(user))
            {
                var approvedByQualityManager = Asset.Team.QualityManager == null || IsApprovedByQualityManager;

                return !ApprovalList.Contains(user) && approvedByQualityManager;
            }

            return false;
        }

        #endregion

        #region Parent/child

        public virtual Version Parent { get; set; }
        public virtual ICollection<Version> Children { get; set; }

        #endregion

        public static Version Create()
        {
            var version = new Version
                              {
                                  ReleaseDate = DateTime.Today,
                                  Attributes = new List<AssetAttributeValue>(),
                                  Children = new List<Version>()
                              };

            return version;
        }
        public static Version Create(PlannedVersion plannedVersion)
        {
            var version = new Version
                              {
                                  Asset = plannedVersion.Asset,
                                  PlannedVersion = plannedVersion,
                                  ReleaseDate = DateTime.Today,
                                  Attributes = new List<AssetAttributeValue>(),
                                  Children = new List<Version>(),
                                  BaseVersion = plannedVersion.BaseVersion,
                                  IsRevision = plannedVersion.IsRevision
                              };
            version.FillAttributeValuesList();

            return version;
        }

        #region Public methods

        

        #endregion

        #region Helper methods

        private void FillAttributeValuesList()
        {
            foreach (var defaultValue in Asset.DefaultTypeValues)
            {
                var value = defaultValue.Copy();
                value.Asset = null;
                value.AssetVersion = this;
                Attributes.Add(value);
            }
        }

        #endregion

        #region Overrides of EntityBase

        public override string Display
        {
            get { return VersionNumber; }
        }

        #endregion
    }
}

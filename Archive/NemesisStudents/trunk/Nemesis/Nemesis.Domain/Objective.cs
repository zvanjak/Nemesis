using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
    public abstract class Objective : IPersistedEntity
    {
      public virtual string Title { get; set; }
      public virtual string Description { get; set; }

      public virtual ObjectivePriority Priority { get; set; }
      public virtual int PercentComplete { get; set; }

      public virtual TeamMember CreatedBy { get; set; }
      public virtual DateTime   CreatedOn { get; set; }

      public virtual Team       AssignedToTeam { get; set; }

      /// <summary>
      /// Dohvaća ili postavlja cilj iz kojeg trenutni slijedi.
      /// </summary>
      //public virtual Objective FollowUpOn { get; set; }
      /// <summary>
      /// Dohvaća ili postavlja cilj u koji je trenutni proslijeđen.
      /// </summary>
//      public virtual Objective FollowedUpIn { get; set; }

      /// <summary>
      /// Dohvaća ili postavlja podatak je li cilj proslijeđen 
      /// u sljedeći interval.
      /// </summary>
      //public virtual bool IsFollowedUp
      //{
      //  get { return FollowedUpIn != null; }
      //}

      /// <summary>
      /// Dohvaća ili postavlja podatak je li cilj odobren.
      /// </summary>
      public virtual bool IsApproved { get; set; }


      /// <summary>
      /// Dohvaća ili postavlja procijenjeno vrijeme izvedbe cilja.
      /// </summary>
      public virtual double EstimatedTime { get; set; }
      /// <summary>
      /// Dohvaća ili postavlja vrijeme potrošeno na izvedbu cilja.
      /// </summary>
      public virtual double ActualTime { get; set; }
      /// <summary>
      /// Dohvaća ili postavlja točnost vremenske procjene cilja..
      /// </summary>
      public virtual double EstimateAccuracy
      {
        get
        {
          if (EstimatedTime > 0.0 && ActualTime > 0.0)
          {
            double difference = EstimatedTime - ActualTime;
            return 100 + (-difference / EstimatedTime) * 100;
          }

          return 0.0;
        }
      }

    }
}

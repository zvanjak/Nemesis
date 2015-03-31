using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using Nemesis.Domain;
using System.Web.Mvc;

namespace Nemesis.Web.Models
{
    public class ActivityViewModel
    {
        private readonly WorkActivity _baseActivity;

        public ActivityViewModel(WorkActivity model)
        {
            this._baseActivity = model;
            Fill();
        }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual string TeamMemberName { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual double EstimatedDuration { get; set; }

        public virtual double ActualDuration { get; set; }

        public virtual double OvertimeDuration { get; set; }

        public virtual string Display { get; set; }

        //public virtual WorkActivityGrade Grade { get; set; }

        protected void Fill()
        {
            this.Title = _baseActivity.Title;
            this.Description = _baseActivity.Description;
            this.TeamMemberName = _baseActivity.DoneBy.Display;
            this.Date = _baseActivity.Date;
            this.EstimatedDuration = _baseActivity.EstimatedDuration;
            this.ActualDuration = _baseActivity.ActualDuration;
            this.OvertimeDuration = _baseActivity.OvertimeDuration;
            this.Display = _baseActivity.Display;
            //this.Grade = _baseActivity.Grade;
        }
    }

    public class ActivityCreateModel
    {
        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual TeamMember TeamMember { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual double ActualDuration { get; set; }

        public virtual bool IsRealizedForObjective { get; set; }

        public virtual int RealizedForObjectiveId { get; set; }

        public virtual MultiSelectList Objectives { get; set; }

        public virtual int WorkOrderId { get; set; }

        public virtual MultiSelectList WorkOrders { get; set; }

    }
}
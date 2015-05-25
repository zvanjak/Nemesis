using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nemesis.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Nemesis.Web.Models.Objective
{
	// TODO - staviti kompletno sve aktivnosti i ciljeve unutra
	public partial class ObjectiveViewModel
	{

        public virtual int Id { get; set; }

        [DisplayName("Parent")]
        public virtual int? ParentId { get; set; }

        public virtual MultiSelectList ParentObjectives { get; set; }

        [DisplayName("Work Order")]
        public virtual int? WorkOrderId { get; set; }

        public virtual MultiSelectList WorkOrders { get; set; }

        [Required]
        public virtual string Title { get; set; }
        
        [Required]
		public virtual string Description { get; set; }
		
        [Required]
        public virtual string Priority { get; set; }

        public virtual IEnumerable<SelectListItem> Priorities
        {
            get
            {
                List<SelectListItem> selectList = new List<SelectListItem>() {
                    new SelectListItem() {Value = ObjectivePriority.HIGH.ToString(), Text = ObjectivePriority.HIGH.ToString()},
                    new SelectListItem() {Value = ObjectivePriority.MEDIUM.ToString(), Text = ObjectivePriority.MEDIUM.ToString()},
                    new SelectListItem() {Value = ObjectivePriority.LOW.ToString(), Text = ObjectivePriority.LOW.ToString()}
                };

                return selectList;
            }
        }

        [Required]
        [DisplayName("Assigned For")]
        public virtual int[] TeamMembersId { get; set; }

        public virtual MultiSelectList TeamMembers { get; set; }

        [DataType(DataType.Duration)]
        [DisplayName("Duration")]
        [Required]
        public virtual double EstimatedTime { get; set; }

        [DisplayName("Percent Complete")]
        public virtual int PercentComplete { get; set; }
	}
}
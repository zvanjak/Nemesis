using Nemesis.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Models
{
    public class WorkOrderViewModel
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EstimatedEndDate { get; set; }

        public virtual int ClientId { get; set; }

        public virtual IEnumerable<SelectListItem> Clients { get; set; }
    }
}
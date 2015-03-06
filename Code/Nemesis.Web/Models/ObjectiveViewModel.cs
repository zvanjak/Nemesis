using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nemesis.Web.Models
{
	// TODO - staviti kompletno sve aktivnosti i ciljeve unutra
	public class ObjectiveViewModel
	{
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual string Priority { get; set; }
	}
}
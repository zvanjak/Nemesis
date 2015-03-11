using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.Domain
{
	public class MonthObjective : Objective
	{
		public virtual int MonthOrdNum { get; set; }

		/// <summary>
		/// Dohvaća podatak je li mjesečni cilj moguće premjestiti u sljedeći tjedan.
		/// </summary>
		public virtual bool IsMoveable
		{
			get
			{
				if (ActualTime == 0.0)
					return true;

				return false;
			}
		}

		/// <summary>
		/// Dohvaća skraćeni tekstualni prikaz mjesečnog cilja.
		/// </summary>
		public string DisplaySummary
		{
			get { return "M" + MonthOrdNum + ", Y" + Year + " (" + AssignedToTeam.Name + ") - " + ShortDescription; }
		}

		public string GetHTMLDisplay()
		{
			var content = new StringBuilder();
			content.Append("<b>MONTH OBJECTIVE</b><br/>");
			content.Append("<b>Short description:</b> " + ShortDescription + "<br/>");
			content.Append("<b>Month:</b> " + MonthOrdNum + "<br/>");
			content.Append("<b>Objective number:</b> " + ObjectiveNumber + "<br/>");
			content.Append("<b>Year:</b> " + Year + "<br/>");
			content.Append("<b>Priority:</b> " + Priority + "<br/>");
//			content.Append("<b>Status:</b> " + Realization + "%<br/>");
			content.Append("<b>Assigned to team:</b> " + AssignedToTeam + "<br/>");
			//if (RealizedForObjective != null)
			//	content.Append("<b>Realized for objective:</b> " + RealizedForObjective + "<br/>");
			content.Append("<b>Estimated time:</b> " + EstimatedTime + "h<br/>");
			content.Append("<b>Description:</b> " + Description + "<br/><br/>");

			// SetActivityTypeHTMLDisplay(content);

			return content.ToString();
		}
	}
}

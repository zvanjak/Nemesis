using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Models.Objective
{
    public class MonthObjectiveViewModel : ObjectiveViewModel
    {
        public int MonthOrdNum { get; set; }

    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Models.Objective
{
    public class WeekObjectiveViewModel : ObjectiveViewModel
    {
        public int WeekOrdNum { get; set; }

        [Required]
        [DisplayName("Parent")]
        public override int ParentId { get; set; }

        public virtual MultiSelectList ParentObjectives { get; set; }
    }
}
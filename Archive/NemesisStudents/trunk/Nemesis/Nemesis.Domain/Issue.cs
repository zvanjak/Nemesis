﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nemesis.Domain
{
    public class Issue : IPersistedEntity
    {
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual string Description { get; set; }

        //public virtual TeamMember AssignedTo { get; set; }
    }
}

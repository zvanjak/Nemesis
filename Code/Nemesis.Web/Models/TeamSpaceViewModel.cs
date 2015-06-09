using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nemesis.Web.Models
{
    public class TeamSpaceViewModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Host { get; set; }
        public virtual string CreatorAspNetId { get; set; }
    }
}
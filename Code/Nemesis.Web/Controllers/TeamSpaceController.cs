using System;
using System.Collections.Generic;
using System.Linq;
using Nemesis.DAL;
using Nemesis.Domain;
using Nemesis.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Controllers
{
    public class TeamSpaceController : Controller
    {
        public ActionResult CreateTeamSpace() {
            TeamSpaceViewModel teamSpace = new TeamSpaceViewModel();
            return View(teamSpace);
        }
    }
}
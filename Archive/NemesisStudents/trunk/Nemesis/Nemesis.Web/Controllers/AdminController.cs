using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Nemesis.Domain.Security;
using Nemesis.Web.Security;

namespace Nemesis.Web.Controllers
{
    
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
				[NemesisAuthorize(UserActionType.ADMIN_HOME)]
        public ActionResult Index()
        {
            return View();
        }

    }
}

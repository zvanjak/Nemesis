using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult AssetList()
        {
            return View();
        }
				public ActionResult AssetListHierarchical()
				{
					return View();
				}
				public ActionResult AssetTypes()
				{
					return View();
				}
				public ActionResult AssetAttributes()
				{
					return View();
				}
		}
}
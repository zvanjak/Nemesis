using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.Domain.Assets;
using Nemesis.DAL;

using Nemesis.Web.Models;

namespace Nemesis.Web.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult AssetList()
        {
					using (var repo = new GenericRepository<Asset>(new NemesisContext()))
					{
						IList<Asset> assetsList = repo.Get().ToList();

						IList<AssetViewModel> assetsView = new List<AssetViewModel>();
						foreach (Asset asset in assetsList)
						{
							assetsView.Add(new AssetViewModel(asset));
						}

						return View(assetsView);
					}
        }
				public ActionResult AssetListHierarchical()
				{
					return View();
				}
				public ActionResult AssetTypes()
				{
					using (var repo = new GenericRepository<AssetType>(new NemesisContext()))
					{
						IList<AssetType> attrList = repo.Get().ToList();

						return View(attrList);
					}
				}

				public ActionResult CreateAssetType()
				{
					AssetTypeViewModel attrVM = new AssetTypeViewModel(new AssetType());

					return View(attrVM);
				}

				[HttpPost]
				public ActionResult CreateAssetType(AssetTypeViewModel entry)
				{
					try
					{
						using (var repo = new GenericRepository<AssetType>(new NemesisContext()))
						{
							AssetType newType = new AssetType();
							newType.Name = entry.Name;

							repo.Insert(newType);
							repo.Save();
						}
						return RedirectToAction("AssetTypes", "Asset");
					}
					catch
					{
						return View();
					}
				}
				public ActionResult AssetAttributes()
				{
					using (var repo = new GenericRepository<AssetAttribute>(new NemesisContext()))
					{
						IList<AssetAttribute> attrList = repo.Get().ToList();

						return View(attrList);
					}
				}

				public ActionResult CreateAssetAttribute()
				{
					AssetAttributeViewModel attrVM = new AssetAttributeViewModel(new AssetAttribute());

					return View(attrVM);
				}

				[HttpPost]
				public ActionResult CreateAssetAttribute(AssetAttributeViewModel entry)
				{
					try
					{
						using (var repo = new GenericRepository<AssetAttribute>(new NemesisContext()))
						{
							AssetAttribute newAttr = new AssetAttribute();
							newAttr.Name = entry.Name;
							newAttr.Type = entry.SelectedType;

							repo.Insert(newAttr);
							repo.Save();
						}
						return RedirectToAction("AssetAttributes", "Asset");
					}
					catch
					{
						return View();
					}
				}
		}
}
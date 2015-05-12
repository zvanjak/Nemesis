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

				[HttpGet]
				// GET: /Client/Create
				public ActionResult CreateAsset()
				{
					Asset newAsset = new Asset();
					AssetViewModel assetVM = new AssetViewModel(newAsset);

					assetVM.Teams = GetTeams();
					assetVM.Types = GetAssetTypes();

					return View(assetVM);
				}

				// POST: /Client/Create
				[HttpPost]
				[ValidateAntiForgeryToken]
				public ActionResult CreateAsset(AssetViewModel assetVM)
				{
					if (ModelState.IsValid)
					{
						var ctx = new NemesisContext();

						using (var repo = new GenericRepository<Asset>(ctx))
						{
							Asset newAsset = new Asset();
							newAsset.Name = assetVM.Name;
							newAsset.Description = assetVM.Description;
							newAsset.PartNumber = assetVM.PartNumber;

							var teamRepo = new GenericRepository<Team>(ctx);
							newAsset.Team = teamRepo.GetByID(assetVM.TeamId);

							var typesRepo = new GenericRepository<AssetType>(ctx);
							newAsset.Type = typesRepo.GetByID(assetVM.TypeId);

							repo.Insert(newAsset);
							repo.Save();
						}
						return RedirectToAction("AssetList", "Asset");
					}
					else
					{
						assetVM.Teams = GetTeams();
					}

					return View(assetVM);
				}

				private IEnumerable<SelectListItem> GetAssetTypes()
				{
					IEnumerable<AssetType> types; // = new List<AssetType>();

					using (var repo = new GenericRepository<AssetType>(new NemesisContext()))
					{
						types = repo.Get();
					}

					return new SelectList(types, "Id", "Display");
				}

				private IEnumerable<SelectListItem> GetTeams()
				{
					IEnumerable<Team> team; // = new List<Team>();

					using (var repo = new GenericRepository<Team>(new NemesisContext()))
					{
						team = repo.Get();
					}

					return new SelectList(team, "Id", "Display");
				}

				private IEnumerable<SelectListItem> GetClients()
				{
					IEnumerable<Client> clients = null;

					using (var repo = new GenericRepository<Client>(new NemesisContext()))
					{
						clients = repo.Get();
					}

					return new SelectList(clients, "Id", "Name");
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
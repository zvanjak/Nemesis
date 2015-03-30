using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using Nemesis.Domain;
using Nemesis.Domain.Assets;

namespace Nemesis.Web.Models
{
	public class AssetViewModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string PartNumber { get; set; }
		public AssetType Type { get; set; }
		public Team Team { get; set; }

		[DisplayName("Assigned to team")]
		public virtual int TeamId { get; set; }

		public virtual IEnumerable<SelectListItem> Teams { get; set; }

		public AssetViewModel(Asset inAsset)
		{
			Name = inAsset.Name;
			Description = inAsset.Description;
			PartNumber = inAsset.PartNumber;
			Type = inAsset.Type;
			
			if( inAsset.Team != null )
				TeamId = inAsset.Team.Id;
		}
	}

	public class AssetAttributeViewModel
	{
		public virtual string Name { get; set; }
		public AssetAttributeType SelectedType { get; set; }

		public List<SelectListItem> AttributeTypes { get; set; }

		public AssetAttributeViewModel()
		{
			AttributeTypes = new List<SelectListItem>();
			foreach (AssetAttributeType t in Enum.GetValues(typeof(AssetAttributeType)))
			{
				AttributeTypes.Add(new SelectListItem() { Value = t.ToString(), Text = t.ToString() });
			}
		}

		public AssetAttributeViewModel(AssetAttribute inAssetAttr)
		{
			Name = inAssetAttr.Name;

			AttributeTypes = new List<SelectListItem>();
			foreach (AssetAttributeType t in Enum.GetValues(typeof(AssetAttributeType)))
			{
				AttributeTypes.Add(new SelectListItem() { Value = t.ToString(), Text = t.ToString() });
			}
		}
	}

	public class AssetTypeViewModel
	{
		public virtual string Name { get; set; }
		public virtual List<AssetAttribute> SelectedAttributes { get; set; }
		public List<SelectListItem> AvailableAttributeTypes { get; set; }

		public AssetTypeViewModel()
		{
			SelectedAttributes = new List<AssetAttribute>();
			AvailableAttributeTypes = new List<SelectListItem>();
		}
		public AssetTypeViewModel(AssetType inType)
		{
			Name = inType.Name;

			SelectedAttributes = new List<AssetAttribute>();
			AvailableAttributeTypes = new List<SelectListItem>();
		}
	}
}
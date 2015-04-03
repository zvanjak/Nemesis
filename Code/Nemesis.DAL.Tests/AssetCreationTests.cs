using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data.Entity;

using Nemesis.Domain;
using Nemesis.Domain.Assets;
using Nemesis.DAL.Configurations;

namespace Nemesis.DAL.Tests
{
	[TestClass]
	public class AssetCreationTests
	{
		[TestMethod]
		public void TestCreateBasicAssetAttributeTypes()
		{
			using (var repo = new GenericRepository<AssetAttribute>(new NemesisContext("NemesisContextTest")))
			{
				Database.SetInitializer(new NemesisInitializer());

				var objStringAttribute = new AssetAttribute() { Type = AssetAttributeType.String, Name = "Source Code path" };
				repo.Insert(objStringAttribute);

				var objLinkAttribute = new AssetAttribute() { Type = AssetAttributeType.Link, Name = "Documentation link" };
				repo.Insert(objLinkAttribute);

				var objEnumAttribute = new AssetAttribute() { Type = AssetAttributeType.Enum, Name = "Platform type" };
				objEnumAttribute.AddEnumValue(".NET 3.0");
				objEnumAttribute.AddEnumValue(".NET 4.5");
				repo.Insert(objEnumAttribute);

				repo.Save();
			}
		}

		[TestMethod]
		public void TestCreateBasicAssetType()
		{

			NemesisContext context = new NemesisContext("NemesisContextTest");

			var aType = new AssetType();
			aType.Name = "HW Component";

			AssetAttribute attr2 = new AssetAttribute() { Name = "HW Platform Type", Type = AssetAttributeType.Enum };
			AssetAttribute attr3 = new AssetAttribute() { Name = "HW Documentation Path", Type = AssetAttributeType.String };
			aType.AddAttribute(attr2);
			aType.AddAttribute(attr3);

			context.AssetTypes.Add(aType);
			context.SaveChanges();
		}

		[TestMethod]
		public void Create_basic_asset_model()
		{
			NemesisContext context = new NemesisContext("NemesisContextTest");

			AssetAttribute attr1 = new AssetAttribute() { Name = "SW Source Code Path", Type = AssetAttributeType.String };
			AssetAttribute attr2 = new AssetAttribute() { Name = "SW Platform Type", Type = AssetAttributeType.Enum };
			AssetAttribute attr3 = new AssetAttribute() { Name = "Documentation Path", Type = AssetAttributeType.String };

			AssetType aType = new AssetType() { Name = "SW Component" };
			aType.AddAttribute(attr1);
			aType.AddAttribute(attr2);
			aType.AddAttribute(attr3);

			Asset prog1 = new Asset() { Name = "Prva komponenta", Description = "Prva testna komponenta koja radi nešto trubo gtx injection", PartNumber = "P12-345", Type = aType };

			context.Assets.Add(prog1);

			context.SaveChanges();
		}

	}
}

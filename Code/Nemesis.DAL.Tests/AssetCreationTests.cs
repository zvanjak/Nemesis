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
		public void TestCreateBasicAssetType()
		{
			using (var repo = new GenericRepository<AssetType>(new NemesisContext("NemesisContextTest")))
			{
				Database.SetInitializer(new NemesisInitializer());

				var obj = new AssetType();
				obj.Name = "HW Component 22";
				//obj.Attributes = new List<AssetAttribute>();

				repo.Insert(obj);
				repo.Save();
			}
		}
		[TestMethod]
		public void TestCreateBasicAssetAttributeTypes()
		{
			using (var repo = new GenericRepository<AssetAttribute>(new NemesisContext("NemesisContextTest")))
			{
				Database.SetInitializer(new NemesisInitializer());

				var objStringAttribute = new AssetAttribute();
				objStringAttribute.Type = AssetAttributeType.String;
				objStringAttribute.Name = "Source Code Path";

				repo.Insert(objStringAttribute);
				repo.Save();

				var objLinkAttribute= new AssetAttribute();
				objLinkAttribute.Type = AssetAttributeType.Link;
				objLinkAttribute.Name = "Documentation link";

				repo.Insert(objLinkAttribute);
				repo.Save();

				var objEnumAttribute = new AssetAttribute();
				objEnumAttribute.Type = AssetAttributeType.Enum;
				objEnumAttribute.Name = "Platform type";
				objEnumAttribute.EnumValues.Add(new AssetAttributeEnumItem() { Name = ".NET 3.0" });
				objEnumAttribute.EnumValues.Add(new AssetAttributeEnumItem() { Name = ".NET 4.5" });

				repo.Insert(objEnumAttribute);
				repo.Save();
			}
		}
	}
}

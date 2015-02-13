using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nemesis.Domain;
using Nemesis.Domain.Assets;

namespace Nemesis.DAL.Test
{
    [TestClass]
    public class AssetTests
    {
        [TestMethod]
        public void TestAddAsset()
        {
            using (var repo = new GenericRepository<Asset>(new NemesisContext()))
            {
                Database.SetInitializer(new NemesisInitializer());

                var respTeam = new Team() {Name = "Responsible team"};
                var prog1 = new Asset() { Name = "Prva komponenta", ResponsibleTeam = respTeam, Description = "Prva", ShortDescription = "Short" };
                var feat1 = new Feature() { BelongsToAsset = prog1, Name = "Feature 1", Description = "Detailed desc", Code = "FEAT-1" };
                var feat2 = new Feature() { BelongsToAsset = prog1, Name = "Feature 2", Description = "Detailed desc", Code = "FEAT-2" };

                prog1.FeatureList.Add(feat1);
                prog1.FeatureList.Add(feat2);

                repo.Insert(prog1);
                repo.Save();
            }
        }
    }
}

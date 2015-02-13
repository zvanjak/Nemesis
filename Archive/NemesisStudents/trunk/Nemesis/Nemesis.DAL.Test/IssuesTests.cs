using System;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nemesis.Domain;
using Nemesis.Domain.Assets;

namespace Nemesis.DAL.Test
{
    [TestClass]
    public class IssuesTests
    {
        [TestMethod]
        public void TestAddBug()
        {
            using (var repo = new GenericRepository<Bug>(new NemesisContext()))
            {
                Database.SetInitializer(new NemesisInitializer());

                var bug1 = new Bug() { Name = "Bug 1", Description = "Detailed desc", Code = "BUG-1" };
                var bug2 = new Bug() { Name = "Bug 2", Description = "Detailed desc", Code = "BUG-2" };

                repo.Insert(bug1);
                repo.Insert(bug2);

                repo.Save();
            }
        }
        [TestMethod]
        public void TestAddFeature()
        {
            using (var repo = new GenericRepository<Feature>(new NemesisContext()))
            {
                Database.SetInitializer(new NemesisInitializer());

                var prog1 = new Asset() { Name = "Komponenta za TestAddFeature", Description = "Prva", ShortDescription = "Short", ResponsibleTeam = new Team() {Name = "Test team za Addfeature test"}};
                var feat1 = new Feature() { BelongsToAsset = prog1, Name = "Feature 1", Description = "Detailed desc", Code = "FEAT-1" };
                var feat2 = new Feature() { BelongsToAsset = prog1, Name = "Feature 2", Description = "Detailed desc", Code = "FEAT-2" };

                prog1.FeatureList.Add(feat1);
                prog1.FeatureList.Add(feat2);

                repo.Insert(feat1);
                repo.Insert(feat2);

                repo.Save();
            }
        }
        [TestMethod]
        public void TestAddRequirement()
        {
            using (var repo = new GenericRepository<Requirement>(new NemesisContext()))
            {
                Database.SetInitializer(new NemesisInitializer());

                var proj1 = new Project() {Name = "Project for TeastAddReq"};
                var req1 = new Requirement() { Name = "Req 1", BelongsToProject = proj1, Description = "Detailed desc", Code = "REQ-1" };
                var req2 = new Requirement() { Name = "Req 2", BelongsToProject = proj1, Description = "Detailed desc", Code = "REQ-2" };

                proj1.ReqsList.Add(req1);
                proj1.ReqsList.Add(req2);

                repo.Insert(req1);
                repo.Insert(req2);

                repo.Save();
            }
        }

    }
}

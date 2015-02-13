using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nemesis.Domain;

namespace Nemesis.DAL.Test
{
    [TestClass]
    public class WorkActivityTests
    {
        [TestMethod]
        public void TestAddActivity()
        {
            using (var repo = new GenericRepository<WorkActivity>(new NemesisContext()))
            {
                WorkActivity act = new WorkActivity() {Date = DateTime.Now, DoneBy = new TeamMember("Radilica")};

                repo.Insert(act);
                repo.Save();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nemesis.BLL.Services;
using Nemesis.DAL;
using Nemesis.Domain.Security;

namespace Nemesis.BLL.Test
{
  [TestClass]
  public class AdministrationServiceTest
  {

    [TestMethod]
    public void CreateUserTest()
    {
      const string firstName = "Ivica";
      const string lastName = "Obestar";
      const string username = "zvone";
      const string password = "developer";
      const string defaultRole = "TEAM_MEMBER";
      try
      {
        AdministrationService.GetInstance().CreateUser(username, password, firstName, lastName, defaultRole);
      }
      catch (Nemesis.BLL.Exceptions.NemesisBaseException)
      {

      }
    }
  }
}

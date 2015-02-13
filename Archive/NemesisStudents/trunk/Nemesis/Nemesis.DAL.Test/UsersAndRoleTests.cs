using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nemesis.Domain.Security;

namespace Nemesis.DAL.Test
{
  [TestClass]
  public class UsersAndRolesTests
  {
    [TestMethod]
    public void DefineRolesActionsTest()
    {
      using (var unitOfWork = new UnitOfWork())
      {
        //Database.SetInitializer(new NemesisInitializer());

				var roleRepository = unitOfWork.RoleRepository;
				var actionRepository = unitOfWork.UserActionRepository;
				var adminRole = ((List<Role>) roleRepository.Get(r => r.RoleName.Equals("ADMIN")))[0];
				var adminHomeAction = ((List<UserAction>)actionRepository.Get(a => a.ActionName.Equals("ADMIN_HOME")))[0];

				adminRole.AllowedActions.Add(adminHomeAction);

        unitOfWork.Save();
      }
    }
  }
}

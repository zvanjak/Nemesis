using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nemesis.BLL.Services;
using Nemesis.Domain.Security;

namespace Nemesis.BLL.Test
{
	[TestClass]
	public class SecurityServiceTest
	{
		[TestMethod]
		public void AuthenticateUserTest()
		{
			const string username = "iobestar";
			const string password = "developer";

			bool result = SecurityService.GetInstance().AuthenticateUser(username, password);
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void AuthorizeUserActionTest()
		{
			const string username = "pperic";
			UserActionType actiontype = UserActionType.ADMIN_HOME;

			bool result = SecurityService.GetInstance().AuthorizeAction(username, actiontype);
			Assert.AreEqual(false, result);
		}
	}
}

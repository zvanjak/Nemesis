using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nemesis.BLL.Utils;
using Nemesis.DAL;
using Nemesis.Domain.Security;

namespace Nemesis.BLL.Services
{
  public sealed class SecurityService
  {
    private static volatile SecurityService instance = null;
    private static object syncObject = new Object();

    public static SecurityService GetInstance()
    {
      if (instance == null)
      {
        lock (syncObject)
        {
          instance = new SecurityService();
        }
      }
      return instance;
    }

    private SecurityService()
    { }

    public bool AuthenticateUser(string username, string password)
    {
      using (var repo = new GenericRepository<User>(new NemesisContext()))
      {
        var users = (List<User>)repo.Get(u => u.Username.Equals(username));

				if (users.Count == 1)
				{
					byte[] validPassword = users[0].Password;
					byte[] salt = users[0].Salt;

					return SecurityUtils.ValidatePassword(password, salt, validPassword);
				}
				return false;
      }
    }

		public bool AuthorizeAction(string username, UserActionType userAction)
		{
			using (var unitOfWork = new UnitOfWork())
			{
				string actionName = Enum.GetName(typeof(UserActionType), userAction);
				var userRepository = unitOfWork.UserRepository;
				var user = userRepository.Get(u => u.Username.Equals(username)).ToList().FirstOrDefault();

				if (user != null)
				{
					var userRoles = user.UserRoles.ToList();
					foreach (var userRole in userRoles)
					{
						var roleActions = userRole.AllowedActions.Select(a => a.ActionName).ToList();
						if(roleActions.Contains(actionName))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public bool IsUserInRole(string username, RoleType roleType)
		{
			using (var unitOfWork = new UnitOfWork())
			{
				string roleName = Enum.GetName(typeof(RoleType), roleType);
				var roleRepository = unitOfWork.RoleRepository;
				var role = roleRepository.Get(r => r.RoleName.Equals(roleName)).ToList().FirstOrDefault();

				if (role != null)
				{
					if (role.RoleUsers.Count(u => u.Username.Equals(username)) > 0)
					{
						return true;
					}
				}
				return false;
			}
		}
  }
}

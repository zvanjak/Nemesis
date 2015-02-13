using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nemesis.DAL;
using Nemesis.Domain.Security;
using Nemesis.BLL.Utils;
using Nemesis.BLL.Exceptions;

namespace Nemesis.BLL.Services
{
  public sealed class AdministrationService
  {

    private static volatile AdministrationService instance = null;
    private static object syncObject = new Object();

    public static AdministrationService GetInstance()
    {
      if (instance == null)
      {
        lock (syncObject)
        {
          instance = new AdministrationService();
        }
      }
      return instance;
    }

    private AdministrationService()
    { }

    public bool CreateUser(string username, string password, 
      string firstName, string lastName, string defaultRoleName)
    {

      using (var unitOfWork = new UnitOfWork())
      {
				Role defaultRole = null;
				List<Role> roles = (List<Role>)unitOfWork.RoleRepository.Get(u => u.RoleName.Equals(defaultRoleName));
        if (roles.Count > 0)
        {
					defaultRole = roles[0];
        }
				else throw new RoleDoesntExist();

				byte[] generatedSalt = SecurityUtils.GenerateSalt();
				byte[] computedSaltedPasswordHash = SecurityUtils.ComputeSaltedPasswordHash(password, generatedSalt);
				// potrebna provjera korisnickog imena!!!
				User newUser = new User();
				newUser.UserRoles.Add(defaultRole);
				newUser.Username = username;
				newUser.Password = computedSaltedPasswordHash;
				newUser.Salt = generatedSalt;

				unitOfWork.UserRepository.Insert(newUser);
				unitOfWork.Save();
				return true;
      }     
    }
  }
}

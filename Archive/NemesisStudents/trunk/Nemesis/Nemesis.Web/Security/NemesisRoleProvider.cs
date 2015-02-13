using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Nemesis.DAL;
using Nemesis.Domain.Security;

namespace Nemesis.Web.Security
{
  public class NemesisRoleProvider : RoleProvider
  {
    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
      throw new NotImplementedException();
    }

    public override string ApplicationName
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public override void CreateRole(string roleName)
    {
      throw new NotImplementedException();
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
      throw new NotImplementedException();
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
      using (var unitOfWork = new UnitOfWork())
      {
        var roleRepository = unitOfWork.RoleRepository;

        var role = roleRepository.Get(r => r.RoleName.Equals(roleName)).ToList().FirstOrDefault();

        if (role != null)
        {
          string[] usersOfRole = role.RoleUsers.Where(u => u.Username.Contains(usernameToMatch)).
            Select(u => u.Username).ToArray();
        }

        return null;
      }
    }

    public override string[] GetAllRoles()
    {
      using (var unitOfWork = new UnitOfWork())
      {
        var roleRepository = unitOfWork.RoleRepository;

        string[] roles = roleRepository.Get(null).Select(u => u.RoleName).ToArray();

        return new string[0];
       }
    }

    public override string[] GetRolesForUser(string username)
    {
      using (var unitOfWork = new UnitOfWork())
      {
        var userRepository = unitOfWork.UserRepository;
        var user = userRepository.Get(u => u.Username.Equals(username)).ToList().FirstOrDefault();

        if (user != null)
        {
          string[] roles = user.UserRoles.Select(r => r.RoleName).ToArray();
          return roles;
        }

        return new string[0];
      }
    }

    public override string[] GetUsersInRole(string roleName)
    {
      using (var unitOfWork = new UnitOfWork())
      {
        var roleRepository = unitOfWork.RoleRepository;
        var role = roleRepository.Get(r => r.RoleName.Equals(roleName)).ToList().FirstOrDefault();

        if (role != null)
        {
          string[] usersOfRole = role.RoleUsers.Select(u => u.Username).ToArray();
          return usersOfRole;
        }

        return new string[0];
      }
    }

    public override bool IsUserInRole(string username, string roleName)
    {
      using (var unitOfWork = new UnitOfWork())
      {
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

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
      throw new NotImplementedException();
    }

    public override bool RoleExists(string roleName)
    {
      using (var unitOfWork = new UnitOfWork())
      {
        var roleRepository = unitOfWork.RoleRepository;
        var role = roleRepository.Get(r => r.RoleName.Equals(roleName)).ToList().FirstOrDefault();

        if (role != null)
        {
          return true;
        }
        return false;
      }
    }
  }
}
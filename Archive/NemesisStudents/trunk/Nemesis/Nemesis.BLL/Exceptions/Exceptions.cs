using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nemesis.BLL.Exceptions
{
  [Serializable]
  public class NemesisBaseException : Exception
  {  }

  [Serializable]
  public class RoleExist : NemesisBaseException
  { }

  [Serializable]
  public class UserExist : NemesisBaseException
  { }

  [Serializable]
  public class RoleDoesntExist : NemesisBaseException
  {  }

  [Serializable]
  public class UserDoesntExist : NemesisBaseException
  { }
  
}

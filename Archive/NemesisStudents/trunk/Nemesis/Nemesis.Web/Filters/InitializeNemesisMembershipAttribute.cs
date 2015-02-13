using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Nemesis.Web.Filters
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public sealed class InitializeNemesisMembershipAttribute : ActionFilterAttribute
  {
    private static NemesisMembershipInitializer _initializer;
    private static object _initializerLock = new object();
    private static bool _isInitialized;

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      // Ensure ASP.NET Simple Membership is initialized only once per app start
      LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
    }

    private class NemesisMembershipInitializer
    {
      public NemesisMembershipInitializer()
      {
        // NemesisMembership initialization
      }
    }
  }
}
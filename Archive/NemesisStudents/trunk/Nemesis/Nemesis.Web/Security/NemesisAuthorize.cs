using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Nemesis.BLL.Services;
using Nemesis.Domain.Security;

namespace Nemesis.Web.Security
{
  public class NemesisAuthorize : AuthorizeAttribute
  {
    private UserActionType _userActionType;

    public NemesisAuthorize(UserActionType userActionType)
    {
      this._userActionType = userActionType;
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
			if (httpContext.User.Identity == null || !httpContext.User.Identity.IsAuthenticated)
			{
				return false;
			}
			bool isAuthorized = SecurityService.GetInstance().AuthorizeAction(httpContext.User.Identity.Name, _userActionType);
			return isAuthorized;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
			HttpContextBase httpContext = filterContext.HttpContext;
			if (filterContext.HttpContext.User.Identity == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
			{
				filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?returnUrl=" +
				filterContext.HttpContext.Server.UrlEncode(filterContext.HttpContext.Request.RawUrl));
			}
			else
			{
				filterContext.Result = new RedirectToRouteResult(new
						RouteValueDictionary(new { controller = "Error", action = "UnauthorizedAction" }));
			}
    }
  }
}
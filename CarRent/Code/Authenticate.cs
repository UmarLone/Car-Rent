using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Codes;
using Repositories;

namespace Codes
{
    /// <summary>
    /// This is authencation filter and used to Authencate User
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthenticateAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            RouteData routeData = filterContext.RouteData;
            var _area = routeData.DataTokens["area"];
            var d = new AllAdminUser();
            var _user = d.FinById(CookieWrapper.AdminId);

            if (_user != null)
            {
                if (_user.AdminID == 0)
                    filterContext.Result = new System.Web.Mvc.RedirectResult("/Admin/");
            }
            else
            {
                filterContext.Result = new System.Web.Mvc.RedirectResult("/Admin/");
            }

            SessionWrapper.Admin = _user;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserAuthenticateAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        //public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        //{
        //    RouteData routeData = filterContext.RouteData;
        //    var _front = routeData.DataTokens["front"];

        //    var d = new AllUser();
        //    var _user = d.FinByUserID(CookieWrapper.FrontUserId);

        //    if (_user != null)
        //    {
        //        if (_user.UserId == 0)
        //            filterContext.Result = new System.Web.Mvc.RedirectResult("~/home/index/");
        //    }
        //    else
        //    {
        //        filterContext.Result = new System.Web.Mvc.RedirectResult("~/home/index/");
        //    }

        //    SessionWrapper.FrontUser = _user;
        //}
    }
}

﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using eUseControl.Domain.Entities.User.UserActionResponse;
using eUseControlBussinessLogic.Interfaces;
using eUseControl.Domain.Enums;
using businessLogic;

namespace eUseControl.Web.Logic.Attributes
{
    public class CustomAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;

        public CustomAuthorizeAttribute()
        {
            var bl = new eUseControlBussinessLogic.BusinesLogic();
            _session = bl.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionKey = HttpContext.Current.Request.Cookies["X-KEY"];

            if (sessionKey == null)
            {
                // No session cookie found, redirect to login
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Auth", action = "Login" }));
                return;
            }

            UserResp profile = _session.GetUserByCookie(sessionKey.Value);

            if (profile == null || !profile.Status)
            {
                // User not authorized, redirect to login
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new { controller = "Auth", action = "Login" }));
            }

            // else let the request continue
        }
    }
}

using eUseControl.Domain.Enums;
using eUseControlBussinessLogic.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace eUseControl.Web.Logic.Attributes
{
    /// <summary>
    /// A custom authorize attribute that checks for the X-KEY cookie, validates the session,
    /// and enforces role-based access.
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly ISession _session;

        public CustomAuthorizeAttribute(string roles = null)
        {
            Roles = roles;

            var bl = new eUseControlBussinessLogic.BusinesLogic();
            _session = bl.GetSessionBL();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var sessionCookie = request.Cookies["X-KEY"];

            if (sessionCookie == null || string.IsNullOrEmpty(sessionCookie.Value))
            {
                RedirectToLogin(filterContext);
                return;
            }

            var profile = _session.GetUserByCookie(sessionCookie.Value);
            if (profile == null || !profile.Status)
            {
                RedirectToLogin(filterContext);
                return;
            }

            if (!string.IsNullOrEmpty(Roles))
            {
                var allowedRoles = Roles.Split(',').Select(r => r.Trim());
                var userRoleName = Enum.GetName(typeof(UserRole), profile.Role);

                if (!allowedRoles.Any(r => r.Equals(userRoleName, StringComparison.OrdinalIgnoreCase)))
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Auth" },
                            { "action", "Login" }
                        });

                    return;
                }
            }

            base.OnAuthorization(filterContext);
        }

        private void RedirectToLogin(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new { controller = "Auth", action = "Login" }
                )
            );
        }
    }
}
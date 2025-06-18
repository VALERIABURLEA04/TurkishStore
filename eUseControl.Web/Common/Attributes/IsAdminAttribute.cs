using eUseControl.BusinessLogic.Services;
using eUseControl.Domain.Enums;
using eUSeControl.BusinessLogic.Dtos.UserDtos;
using eUSeControl.BusinessLogic.Services;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eUseControl.Web.Logic.Attributes
{
    public class IsAdminAttribute : ActionFilterAttribute
    {
        private readonly ISessionService _session;

        public IsAdminAttribute()
        {
            _session = new SessionService();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionKey = HttpContext.Current.Request.Cookies["X-KEY"];

            if (sessionKey != null)
            {
                UserRespDto profile = _session.GetUserByCookie(sessionKey.Value);

                if (profile != null && profile.Role != UserRole.Admin)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { controller = "Admin", action = "Dashboard" }));
                }
            }
        }
    }
}
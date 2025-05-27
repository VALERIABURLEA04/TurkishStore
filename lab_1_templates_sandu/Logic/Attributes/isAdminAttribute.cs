using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using eUseControl.Domain.Entities.User.UserActionResponse;
using eUseControlBussinessLogic.Interfaces;
using eUseControl.Domain.Enums;
using businessLogic;

namespace lab_1_templates_sandu.Logic.Attributes
{
	public class isAdminAttribute : ActionFilterAttribute
	{
        private readonly ISession _session;
        public isAdminAttribute()
        {
            var bl = new eUseControlBussinessLogic.BusinesLogic();
            _session = bl.GetSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionKey = HttpContext.Current.Request.Cookies["X-KEY"];

            if (sessionKey != null)
            {
                UserResp profile = _session.GetUserByCookie(sessionKey.Value);

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
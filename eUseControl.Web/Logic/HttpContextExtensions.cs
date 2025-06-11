using businessLogic.Dtos.UserDtos;
using System.Web;

namespace eUseControl.Web.Logic
{
    public static class HttpContextExtensions
    {
        public static UserRespDto GetUserProfile(this HttpContext context)
        {
            return (UserRespDto)context?.Session["__SessionObject"];
        }

        public static void SetUserProfile(this HttpContext context, UserRespDto data)
        {
            context.Session.Add("__SesionObject", data);
        }
    }
}
using System;
using System.Web;

namespace eUSeControl.BusinessLogic.Dtos.UserDtos
{
    public class UserCookieRespDto
    {
        public HttpCookie Cookie { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; } = true;
    }
}
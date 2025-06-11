using System;
using System.Web;

namespace eUseControl.Domain.Entities.UserEntities
{
    public class UserCookieResp
    {
        public HttpCookie Cookie { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; } = true;
    }
}
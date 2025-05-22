using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.Domain.Entities.User.UserActionResponse
{
    public class UserCookieResp
    {
        public HttpCookie Cookie { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; } = true;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.User.UserActionResponse;

namespace eUseControlBussinessLogic.Interfaces
{
    public interface ISession
    {
        UserResp LogInLogic(UserLoginData data);
        UserCookieResp GenerateCookieByUser(int id);
        UserResp GetUserByCookie(string sessionKey);
    }
}

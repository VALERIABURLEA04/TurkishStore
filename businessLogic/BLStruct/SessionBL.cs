using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.User.UserActionResponse;
using eUseControlBussinessLogic.Core;
using eUseControlBussinessLogic.Interfaces;

namespace businessLogic.BLStruct
{
    public class SessionBL : UserApi, ISession
    {
        public UserCookieResp GenerateCookieByUser(int id)
        {
            return GenerateCookieByUserAction(id);
        }

        public UserResp GetUserByCookie(string sessionKey)
        {
            return GetUserByCookieAction(sessionKey);
        }

        public UserResp LogInLogic(UserLoginData data)
        {
            return LogInUser(data);
        }
    }
}

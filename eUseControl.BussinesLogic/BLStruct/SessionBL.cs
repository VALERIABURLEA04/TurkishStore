using businessLogic.Dtos.UserDtos;
using eUseControlBussinessLogic.Core;
using eUseControlBussinessLogic.Interfaces;

namespace businessLogic.BLStruct
{
    public class SessionBL : UserApi, ISession
    {
        public UserCookieRespDto GenerateCookieByUser(int id)
        {
            return GenerateCookieByUserAction(id);
        }

        public UserRespDto GetUserByCookie(string sessionKey)
        {
            return GetUserByCookieAction(sessionKey);
        }

        public UserRespDto LogInLogic(UserLoginDto data)
        {
            return LogInUser(data);
        }
    }
}
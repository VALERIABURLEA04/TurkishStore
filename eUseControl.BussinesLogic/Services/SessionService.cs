using eUseControl.BusinessLogic.Services;
using eUSeControl.BusinessLogic.Dtos.UserDtos;
using eUseControlBussinessLogic.Core;

namespace eUSeControl.BusinessLogic.Services
{
    public class SessionService : UserApi, ISessionService
    {
        private static SessionService _instance;
        private static readonly object _lock = new object();

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

        public static SessionService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new SessionService();
                }
            }

            return _instance;
        }
    }
}
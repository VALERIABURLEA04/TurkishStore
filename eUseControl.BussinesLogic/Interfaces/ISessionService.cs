using eUSeControl.BusinessLogic.Dtos.UserDtos;

namespace eUseControl.BusinessLogic.Services
{
    public interface ISessionService
    {
        UserRespDto LogInLogic(UserLoginDto data);

        UserCookieRespDto GenerateCookieByUser(int id);

        UserRespDto GetUserByCookie(string sessionKey);
    }
}
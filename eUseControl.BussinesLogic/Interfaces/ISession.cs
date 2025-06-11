using businessLogic.Dtos.UserDtos;

namespace eUseControlBussinessLogic.Interfaces
{
    public interface ISession
    {
        UserRespDto LogInLogic(UserLoginDto data);

        UserCookieRespDto GenerateCookieByUser(int id);

        UserRespDto GetUserByCookie(string sessionKey);
    }
}
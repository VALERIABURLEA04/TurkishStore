using businessLogic.Dtos.UserDtos;

namespace businessLogic.Interfaces
{
    public interface IRegister
    {
        string SignUpLogic(UserRegisterDto data);
    }
}
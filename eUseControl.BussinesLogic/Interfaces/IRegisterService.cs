using eUSeControl.BusinessLogic.Dtos.UserDtos;

namespace eUSeControl.BusinessLogic.Interfaces
{
    public interface IRegisterService
    {
        string SignUpLogic(UserRegisterDto data);
    }
}
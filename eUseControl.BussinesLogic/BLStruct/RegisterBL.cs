using businessLogic.Dtos.UserDtos;
using businessLogic.Interfaces;
using eUseControlBussinessLogic.Core;

namespace businessLogic.BLStruct
{
    public class RegisterBL : UserApi, IRegister
    {
        public string SignUpLogic(UserRegisterDto data)
        {
            return RegisterUser(data);

        }
    }

}
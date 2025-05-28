using businessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControlBussinessLogic.Core;

namespace businessLogic.BLStruct
{
    public class RegisterBL : UserApi, IRegister
    {
        public string SignUpLogic(UserRegisterData data)
        {
            return RegisterUser(data);
        }
    }
}
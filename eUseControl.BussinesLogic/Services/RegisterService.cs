using eUSeControl.BusinessLogic.Dtos.UserDtos;
using eUSeControl.BusinessLogic.Interfaces;
using eUseControlBussinessLogic.Core;

namespace eUSeControl.BusinessLogic.Services
{
    public class RegisterService : UserApi, IRegisterService
    {
        private static RegisterService _instance;
        private static readonly object _lock = new object();

        public string SignUpLogic(UserRegisterDto data)
        {
            return RegisterUser(data);
        }

        public static RegisterService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new RegisterService();
                }
            }

            return _instance;
        }
    }
}
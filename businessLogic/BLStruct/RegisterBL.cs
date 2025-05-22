using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
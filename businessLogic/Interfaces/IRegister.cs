using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;

namespace businessLogic.Interfaces
{
    public interface IRegister
    {
        string SignUpLogic(UserRegisterData data);
    }
}

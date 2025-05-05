using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControlBussinessLogic.Interfaces
{
    public  interface ISession
    {
        void StartSession(string userId);
        void EndSession();
    }
}

/* using businessLogic.BLStruct;
using businessLogic.Interfaces;
using eUseControlBussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControlBussinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IRegister GetRegisterBL()
        {
            return new RegisterBL();
        }
    }
}
*/
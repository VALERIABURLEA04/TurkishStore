using businessLogic.BLStruct;
using businessLogic.Interfaces;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControlBussinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControlBussinessLogic
{
    public class BusinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IRegister GetRegisterBL()
        {
            return new RegisterBL();
        }

        public IContact GetContactBL()
        {
            return new ContactBL();
        }

        public IProductRepository GetProductRepository()
        {
            return new ProductRepositoryBL();
        }

        public IUser GetUserBL()
        {
            return new UserBL();
        }
    }
}

using businessLogic.BLStruct;
using businessLogic.Interfaces;
using businessLogic.Interfaces.Repositories;
using eUseControlBussinessLogic.Interfaces;

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

        public IBlogPostRepository GetBlogPostRepository()
        {
            return new BlogPostRepository();
        }
    }
}
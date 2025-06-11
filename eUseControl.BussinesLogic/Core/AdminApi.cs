using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.ProductEntities;
using System.Collections.Generic;
using System.Linq;

namespace eUseControlBussinessLogic.Core
{
    public class AdminApi
    {
        public List<Product> GetProductsList()
        {
            using (var context = new DataContext())
            {
                List<Product> products = context.Products.ToList();

                if (products == null && !products.Any())
                    return new List<Product>();

                return products;
            }
        }
    }
}
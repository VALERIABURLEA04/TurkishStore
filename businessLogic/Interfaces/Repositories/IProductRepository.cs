using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eUseControl.Domain.Entities.Product;

namespace businessLogic.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product, HttpPostedFileBase image);
        void UpdateProduct(Product product, HttpPostedFileBase image, bool? removeImage);
        void DeleteProduct(int id);
        void DeleteImage(int id);
    }
}

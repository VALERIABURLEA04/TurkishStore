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
        List<ProductDataEntities> GetAllProducts();
        ProductDataEntities GetProductById(int id);
        void AddProduct(ProductDataEntities product, HttpPostedFileBase image);
        void UpdateProduct(ProductDataEntities product, HttpPostedFileBase image, bool? removeImage);
        void DeleteProduct(int id);
        void DeleteImage(int id);

        IEnumerable<ProductDataEntities> Search(string query);
    }
}
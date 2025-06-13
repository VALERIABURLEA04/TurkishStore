using businessLogic.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace businessLogic.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDto> GetByIdAsync(int id);

        Task<List<ProductDto>> GetAllProducts(int userId);

        ProductDto GetProductById(int id);

        ProductDetailsDto GetProductDetailsById(int id);

        void AddProduct(ProductDto product, HttpPostedFileBase image);

        void UpdateProduct(ProductDto product, HttpPostedFileBase image, bool? removeImage);

        void DeleteProduct(int id);

        void DeleteImage(int id);

        bool UpdateProductToFavorite(int userId, int productId);

        List<int> GetFavoriteProductIds(int userId);

        List<ProductDto> GetProductsByIds(List<int> productIds);
    }
}
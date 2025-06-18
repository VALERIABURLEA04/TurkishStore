using eUseControl.Domain.Entities.ProductEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUseControl.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<List<Product>> GetAllProductsAsync();

        Task<bool> AddProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<bool> UpdateProductToFavoriteAsync(int userId, int productId);

        List<int> GetFavoriteProductsIds(int userId);

        Task<List<int>> GetFavoriteProductIdsAsync(int userId);

        Task<List<Product>> GetProductsByIdsAsync(List<int> productIds);

        Task RemoveImageAsync(ProductImage image);
    }
}
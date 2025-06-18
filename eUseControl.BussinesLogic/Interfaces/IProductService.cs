using eUSeControl.BusinessLogic.Dtos.ProductDtos;
using eUSeControl.BusinessLogic.eUSeControl.BusinessLogic.Dtos.ProducteUSeControl.BusinessLogic.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUSeControl.BusinessLogic.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(int productId, int userId);

        Task<List<ProductDto>> GetAllProductsAsync(int userId);

        Task<ProductDetailsDto> GetProductDetailsByIdAsync(int productId, int userId = 0);

        Task<bool> UpdateProductToFavoriteAsync(int userId, int productId);

        Task<List<int>> GetFavoriteProductIdsAsync(int userId);

        Task<List<ProductDto>> GetProductsByIdsAsync(List<int> productIds, int userId);

        Task<bool> AddProductAsync(UpsertProductDto model);

        Task<bool> UpdateProductAsync(UpsertProductDto model);

        Task<bool> DeleteProductAsync(int id);

        int GetFavoriteProductsCount(int userId);
    }
}
using businessLogic.Dtos.CartDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUSeControl.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        int GetProductsFromCartByUserId(int userId);

        Task<List<CartItemDto>> GetCartItemsAsync(int userId);

        Task<string> AddCartItemAsync(UpsertCartItemDto model);

        Task<string> RemoveCartItemAsync(int cartItemId);

        Task<string> RemoveAllCartItemsAsync(int userId);

        Task<int> UpdateCartItemQuantityAsync(int userId, int cartId, int quantity);
    }
}
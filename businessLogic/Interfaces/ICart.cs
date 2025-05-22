using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Cart;

namespace businessLogic.Interfaces
{
    public interface ICart
    {
        Task<decimal> CalculateTotalAsync(int userId);
        Task<int> AddItemAsync(int userId, int productId, int quantity);
        Task<bool> RemoveItemAsync(int userId, int productId);
        Task<bool> UpdateQuantityAsync(int userId, int productId, int quantity);
        Task<List<CartItem>> GetBasketItemsAsync(int userId);
        Task<bool> ClearBasketAsync(int userId);
    }
}

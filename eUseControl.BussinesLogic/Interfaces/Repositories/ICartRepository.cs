using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Cart;

namespace businessLogic.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetItemsByUserIdAsync(int userId);
        Task<CartItem> GetItemByUserIdAndProductIdAsync(int userId, int productId);
        Task<int> CreateAsync(CartItem item);
        Task<bool> UpdateAsync(CartItem item);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAllByUserIdAsync(int userId);
    }
}

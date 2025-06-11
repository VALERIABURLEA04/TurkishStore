using eUseControl.Domain.Entities.ListingEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

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
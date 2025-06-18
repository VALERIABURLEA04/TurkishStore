using eUseControl.Domain.Entities.ListingEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eUseControl.Domain.Repositories
{
    public interface ICartRepository : IDisposable
    {
        Task<List<Cart>> GetCartItemsByUserIdAsync(int userId);

        List<Cart> GetCartItemsByUserId(int userId);

        Task<Cart> GetItemByUserIdAndProductIdAsync(int userId, int productId);

        Task<int> CreateAsync(Cart item);

        Task<bool> UpdateAsync(Cart item);

        Task<bool> DeleteAsync(int id);

        Task<bool> DeleteAllByUserIdAsync(int userId);
    }
}
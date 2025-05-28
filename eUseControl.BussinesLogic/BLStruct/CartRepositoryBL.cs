using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using businessLogic.Interfaces.Repositories;
using eUseControl.Domain.Entities.Cart;
using BusinessLogic.DBModel;

namespace businessLogic.BLStruct
{
    public class CartRepositoryBL : ICartRepository
    {
        private readonly DataContext _context;

        public CartRepositoryBL()
        {
            _context = new DataContext();
        }

        public async Task<int> CreateAsync(CartItem item)
        {
            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<bool> DeleteAllByUserIdAsync(int userId)
        {
            var items = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            if (!items.Any()) return false;

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartItem> GetItemByUserIdAndProductIdAsync(int userId, int productId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);
        }

        public async Task<List<CartItem>> GetItemsByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(CartItem item)
        {
            var existingItem = await _context.CartItems.FindAsync(item.Id);
            if (existingItem == null) return false;

            // Update properties
            existingItem.Quantity = item.Quantity;
            existingItem.ProductId = item.ProductId;
            existingItem.UserId = item.UserId;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
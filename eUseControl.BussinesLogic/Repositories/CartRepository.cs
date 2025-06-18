using eUseControl.Domain.Entities.ListingEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace eUSeControl.DataAccess.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly EUseControlDbContext _context;
        private static CartRepository _instance;
        private static readonly object _lock = new object();
        private bool _disposed;

        private CartRepository()
        {
            _context = EUseControlDbContext.GetInstance();
        }

        public static CartRepository GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new CartRepository();
                }
            }
            return _instance;
        }

        public async Task<List<Cart>> GetCartItemsByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.User)
                .Include(c => c.Size)
                .Include(c => c.Color)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Cart> GetItemByUserIdAndProductIdAsync(int userId, int productId)
        {
            return await _context.CartItems
                .Include(c => c.Product)
                .Include(c => c.Size)
                .Include(c => c.Color)
                .SingleOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        }

        public async Task<int> CreateAsync(Cart item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();

            return item.Id;
        }

        public async Task<bool> UpdateAsync(Cart item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.CartItems.FindAsync(id);
            if (entity == null)
                return false;

            _context.CartItems.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAllByUserIdAsync(int userId)
        {
            var items = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!items.Any())
                return false;

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();

            return true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<Cart> GetCartItemsByUserId(int userId)
        {
            if (userId <= 0)
                return new List<Cart>();

            return _context.CartItems
                .Where(c => c.UserId == userId)
                .ToList();
        }
    }
}
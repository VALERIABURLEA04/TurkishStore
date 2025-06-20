using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.DataAccess.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.DataAccesss.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EUseControlDbContext _context;

        private static ProductRepository _instance;
        private static readonly object _lock = new object();

        private ProductRepository()
        {
            _context = EUseControlDbContext.GetInstance();
        }

        public static ProductRepository GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ProductRepository();
                }
            }
            return _instance;
        }

        public List<int> GetFavoriteProductsIds(int userId)
        {
            return _context.ProductsToUsers
                .Where(pu => pu.UserId == userId && pu.IsFavorite)
                .Select(pu => pu.ProductId)
                .ToList();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(x => x.ProductColors)
                .Include(x => x.ProductSizes)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductsToUsers)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(x => x.ProductColors)
                .Include(x => x.ProductSizes)
                .Include(x => x.ProductImages)
                .Include(x => x.ProductsToUsers)
                .ToListAsync();
        }

        public async Task<bool> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateProductAsync(Product updated)
        {
            if (updated == null || updated.Id == 0)
                return false;

            var existing = await _context.Products
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == updated.Id);

            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(updated);

            _context.ProductSizes.RemoveRange(existing.ProductSizes);
            _context.ProductColors.RemoveRange(existing.ProductColors);
            _context.ProductImages.RemoveRange(existing.ProductImages);
            await _context.SaveChangesAsync();

            foreach (var sz in updated.ProductSizes)
            {
                _context.ProductSizes.Add(new ProductSize
                {
                    SizeValue = sz.SizeValue,
                    ProductId = existing.Id
                });
            }

            foreach (var cl in updated.ProductColors)
            {
                _context.ProductColors.Add(new ProductColor
                {
                    ColorValue = cl.ColorValue,
                    ProductId = existing.Id
                });
            }

            foreach (var img in updated.ProductImages)
            {
                _context.ProductImages.Add(new ProductImage
                {
                    ImageUrl = img.ImageUrl,
                    SortOrder = img.SortOrder,
                    ProductId = existing.Id
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return false;

            foreach (var img in product.ProductImages.ToList())
            {
                var fileName = System.IO.Path.GetFileName(img.ImageUrl);
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + fileName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductToFavoriteAsync(int userId, int productId)
        {
            var entry = await _context.ProductsToUsers
                .SingleOrDefaultAsync(x => x.ProductId == productId && x.UserId == userId);

            if (entry != null)
                entry.IsFavorite = !entry.IsFavorite;
            else
                _context.ProductsToUsers.Add(new ProductToUser { ProductId = productId, UserId = userId, IsFavorite = true });

            await _context.SaveChangesAsync();
            return entry?.IsFavorite ?? true;
        }

        public async Task<List<int>> GetFavoriteProductIdsAsync(int userId)
        {
            return await _context.ProductsToUsers
                .Where(pu => pu.UserId == userId && pu.IsFavorite)
                .Select(pu => pu.ProductId)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByIdsAsync(List<int> productIds)
        {
            return await _context.Products
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .Where(p => productIds.Contains(p.Id))
                .ToListAsync();
        }

        public async Task RemoveImageAsync(ProductImage image)
        {
            var path = HttpContext.Current.Server.MapPath(image.ImageUrl);
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}
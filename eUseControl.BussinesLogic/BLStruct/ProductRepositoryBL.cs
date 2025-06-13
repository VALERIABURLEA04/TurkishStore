using businessLogic.Dtos.ProductDtos;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.ProductEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace businessLogic.BLStruct
{
    public class ProductRepositoryBL : IProductRepository, IDisposable
    {
        private readonly EUseControlDbContext _context;
        private bool _disposed;

        public ProductRepositoryBL()
        {
            _context = new EUseControlDbContext();
        }

        // Internal mapper for summary DTO
        private ProductDto MapToDto(Product x, int userId)
        {
            return new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Category = x.Category.ToString(),
                ImageUrl = x.ProductImages
                            .OrderBy(pi => pi.SortOrder)
                            .Select(pi => pi.ImageUrl)
                            .FirstOrDefault(),
                Price = x.Price,
                IsFavorite = x.ProductsToUsers
                            .FirstOrDefault(pu => pu.UserId == userId)?.IsFavorite ?? false,
                Stock = x.Stock
            };
        }

        // Get by id async (returns summary DTO)
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var x = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .FirstOrDefaultAsync(p => p.Id == id);
            return x == null ? null : MapToDto(x, 0);
        }

        // Get all products
        public async Task<List<ProductDto>> GetAllProducts(int userId)
        {
            var list = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .ToListAsync();
            return list.Select(p => MapToDto(p, userId)).ToList();
        }

        // Get single summary DTO synchronously
        public ProductDto GetProductById(int id)
        {
            var x = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .FirstOrDefault(p => p.Id == id);
            return x == null ? null : MapToDto(x, 0);
        }

        // Get detail DTO by id, including sizes and colors
        public ProductDetailsDto GetProductDetailsById(int id)
        {
            var x = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .FirstOrDefault(p => p.Id == id);
            if (x == null) return null;

            var dto = new ProductDetailsDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                FullDescription = x.FullDescription,
                Price = x.Price,
                Sku = x.Sku,
                Category = x.Category,
                Stock = x.Stock,
                Weight = x.Weight,
                Dimensions = x.Dimensions,
                Materials = x.Materials,
                ImageUrls = x.ProductImages
                             .OrderBy(pi => pi.SortOrder)
                             .Select(pi => pi.ImageUrl)
                             .ToList(),
                AvailableSizes = x.ProductSizes
                             .OrderBy(ps => ps.Id)
                             .Select(ps => ps.SizeValue)
                             .ToList(),
                AvailableColors = x.ProductColors
                             .OrderBy(pc => pc.Id)
                             .Select(pc => pc.ColorValue)
                             .ToList(),
                IsFavorite = x.ProductsToUsers
                             .FirstOrDefault(pu => pu.UserId == 0)?.IsFavorite ?? false,
                ReviewCount = 10,
                RelatedProducts = GetRelatedProducts(x.CategoryValue, x.Id, 0)
            };

            return dto;
        }

        // Toggle favorite
        public bool UpdateProductToFavorite(int userId, int productId)
        {
            var entry = _context.Set<ProductToUser>()
                .SingleOrDefault(x => x.ProductId == productId && x.UserId == userId);
            if (entry != null)
                entry.IsFavorite = !entry.IsFavorite;
            else
                _context.Set<ProductToUser>().Add(new ProductToUser
                {
                    ProductId = productId,
                    UserId = userId,
                    IsFavorite = true
                });
            _context.SaveChanges();
            return entry?.IsFavorite ?? true;
        }

        // Get favorite IDs
        public List<int> GetFavoriteProductIds(int userId)
        {
            if (userId == 0) return new List<int>();
            return _context.Set<ProductToUser>()
                .Where(x => x.UserId == userId && x.IsFavorite)
                .Select(x => x.ProductId)
                .ToList();
        }

        // Get products by IDs
        public List<ProductDto> GetProductsByIds(List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
                return new List<ProductDto>();
            var list = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .Where(p => productIds.Contains(p.Id))
                .ToList();
            return list.Select(p => MapToDto(p, 0)).ToList();
        }

        // Add new product
        public void AddProduct(ProductDto model, HttpPostedFileBase image)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                FullDescription = model.FullDescription,
                Price = model.Price,
                Sku = model.Sku,
                CategoryValue = model.Category.ToString(),
                Stock = model.Stock,
                Weight = model.Weight,
                Dimensions = model.Dimensions,
                Materials = model.Materials
            };
            if (image != null && image.ContentLength > 0)
            {
                var fn = Path.GetFileName(image.FileName);
                var un = Guid.NewGuid() + Path.GetExtension(fn);
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + un);
                image.SaveAs(path);
                product.ProductImages.Add(new ProductImage { ImageUrl = "/Content/images/" + un, SortOrder = 1 });
            }
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Update product
        public void UpdateProduct(ProductDto model, HttpPostedFileBase image, bool? removeImage)
        {
            var existing = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == model.Id);
            if (existing == null) return;

            existing.Name = model.Name;
            existing.Description = model.Description;
            existing.FullDescription = model.FullDescription;
            existing.Price = model.Price;
            existing.Sku = model.Sku;
            existing.CategoryValue = model.Category.ToString();
            existing.Stock = model.Stock;
            existing.Weight = model.Weight;
            existing.Dimensions = model.Dimensions;
            existing.Materials = model.Materials;

            var img = existing.ProductImages.FirstOrDefault();
            if (removeImage == true && img != null)
            {
                var old = HttpContext.Current.Server.MapPath(img.ImageUrl);
                if (File.Exists(old)) File.Delete(old);
                _context.Set<ProductImage>().Remove(img);
            }
            if (image != null && image.ContentLength > 0)
            {
                if (img != null)
                {
                    var old = HttpContext.Current.Server.MapPath(img.ImageUrl);
                    if (File.Exists(old)) File.Delete(old);
                    img.ImageUrl = null;
                }
                var fn = Path.GetFileName(image.FileName);
                var un = Guid.NewGuid() + Path.GetExtension(fn);
                var pth = HttpContext.Current.Server.MapPath("~/Content/images/" + un);
                image.SaveAs(pth);
                if (img != null)
                    img.ImageUrl = "/Content/images/" + un;
                else
                    existing.ProductImages.Add(new ProductImage { ImageUrl = "/Content/images/" + un, SortOrder = 1 });
            }
            _context.SaveChanges();
        }

        // Delete product and images
        public void DeleteProduct(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return;
            foreach (var img in product.ProductImages.ToList())
            {
                var p = HttpContext.Current.Server.MapPath(img.ImageUrl);
                if (File.Exists(p)) File.Delete(p);
                _context.Set<ProductImage>().Remove(img);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        // Delete only image
        public void DeleteImage(int id)
        {
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            var img = product?.ProductImages.FirstOrDefault();
            if (img == null) return;
            var path = HttpContext.Current.Server.MapPath(img.ImageUrl);
            if (File.Exists(path)) File.Delete(path);
            _context.Set<ProductImage>().Remove(img);
            _context.SaveChanges();
        }

        // Helper for related products
        private List<ProductDto> GetRelatedProducts(string category, int excludeId, int userId)
        {
            return _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductsToUsers)
                .Where(p => p.CategoryValue == category && p.Id != excludeId)
                .Take(8)
                .AsEnumerable()
                .Select(p => MapToDto(p, userId))
                .ToList();
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
    }
}
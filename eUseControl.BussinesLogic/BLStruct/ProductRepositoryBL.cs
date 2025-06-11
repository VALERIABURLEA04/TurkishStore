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
    public class ProductRepositoryBL : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepositoryBL()
        {
            _context = new DataContext();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _context.Products
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    IsFavorite = x.ProductsToUsers.FirstOrDefault(s => s.UserId == id).IsFavorite,
                    Price = x.Price
                })
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<ProductDto> GetAllProducts(int userId)
        {
            return _context.Products
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    IsFavorite = x.ProductsToUsers.FirstOrDefault(s => s.UserId == userId).IsFavorite,
                    Price = x.Price
                })
                .ToList();
        }

        public ProductDto GetProductById(int id)
        {
            return _context.Products
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    IsFavorite = x.ProductsToUsers.FirstOrDefault(s => s.UserId == id).IsFavorite,
                    Price = x.Price
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public void AddProduct(ProductDto model, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var uniqueFileName = Guid.NewGuid() + "_" + fileName;
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + uniqueFileName);
                image.SaveAs(path);
                model.ImageUrl = "/Content/images/" + uniqueFileName;
            }

            Product product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(ProductDto product, HttpPostedFileBase image, bool? removeImage)
        {
            var existing = _context.Products.Find(product.Id);
            if (existing == null) return;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;

            // Remove existing image
            if (removeImage == true && !string.IsNullOrEmpty(existing.ImageUrl))
            {
                var oldPath = HttpContext.Current.Server.MapPath(existing.ImageUrl);
                if (File.Exists(oldPath)) File.Delete(oldPath);
                existing.ImageUrl = null;
            }

            // Upload new image
            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(fileName);
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + uniqueFileName);
                image.SaveAs(path);

                // Delete old image if needed
                if (!string.IsNullOrEmpty(existing.ImageUrl))
                {
                    var oldImagePath = HttpContext.Current.Server.MapPath(existing.ImageUrl);
                    if (File.Exists(oldImagePath)) File.Delete(oldImagePath);
                }

                existing.ImageUrl = "/Content/images/" + uniqueFileName;
            }

            _context.SaveChanges();
        }

        public void DeleteImage(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null || string.IsNullOrEmpty(product.ImageUrl)) return;

            try
            {
                var fullPath = HttpContext.Current.Server.MapPath(product.ImageUrl);
                if (File.Exists(fullPath)) File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error deleting image: " + ex.Message);
            }

            product.ImageUrl = null;
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var path = HttpContext.Current.Server.MapPath(product.ImageUrl);
                if (File.Exists(path)) File.Delete(path);
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public bool UpdateProductToFavorite(int userId, int productId)
        {
            var entry = _context.ProductsToUsers
                .SingleOrDefault(x => x.ProductId == productId && x.UserId == userId);

            if (entry != null)
            {
                entry.IsFavorite = !entry.IsFavorite;
            }
            else
            {
                entry = new ProductToUser
                {
                    ProductId = productId,
                    UserId = userId,
                    IsFavorite = true
                };

                _context.ProductsToUsers.Add(entry);
            }

            _context.SaveChanges();

            return entry.IsFavorite;
        }

        public List<int> GetFavoriteProductIds(int userId)
        {
            if (userId == 0)
                return new List<int>();

            var result = _context.ProductsToUsers
                .Where(x => x.UserId == userId)
                .Select(x => x.ProductId)
                .ToList();

            return result;
        }

        public List<ProductDto> GetProductsByIds(List<int> productIds)
        {
            if (productIds == null || !productIds.Any())
                return new List<ProductDto>();

            var products = _context.Products
                .Where(x => productIds.Contains(x.Id))
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    IsFavorite = true,
                    Price = x.Price
                })
                .ToList();

            return products;
        }
    }
}
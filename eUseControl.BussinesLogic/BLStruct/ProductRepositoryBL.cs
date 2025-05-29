using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using businessLogic.Interfaces.Repositories;
using BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Product;

namespace businessLogic.BLStruct
{
    public class ProductRepositoryBL : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepositoryBL()
        {
            _context = new DataContext();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public List<ProductDataEntities> GetAllProducts()
        {
            using (var db = new DataContext())
            {
                // Check if there are any products
                if (db.Products.Any())
                {
                    
                    return db.Products.Select(p => new ProductDataEntities
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl

                    }).ToList();
                }
                else
                {
                    // Return an empty list if there are no products
                    return new List<ProductDataEntities>();
                }
            }
        }


        public ProductDataEntities GetProductById(int id)
        {
            using (var db = new DataContext())
            {
                var product = db.Products.Find(id);
                if (product == null) return null;

                // Manual mapping to business entity
                return new ProductDataEntities
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl
                };
            }
        }

        public void AddProduct(ProductDataEntities product, HttpPostedFileBase image)
        {
            var dbProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var uniqueFileName = Guid.NewGuid() + "_" + fileName;
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + uniqueFileName);
                image.SaveAs(path);
                dbProduct.ImageUrl = "/Content/images/" + uniqueFileName;
            }

            _context.Products.Add(dbProduct);
            _context.SaveChanges();
        }


        public void UpdateProduct(ProductDataEntities product, HttpPostedFileBase image, bool? removeImage)
        {
            var existing = _context.Products.Find(product.Id);
            if (existing == null) return;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;

            if (removeImage == true && !string.IsNullOrEmpty(existing.ImageUrl))
            {
                var oldPath = HttpContext.Current.Server.MapPath(existing.ImageUrl);
                if (File.Exists(oldPath)) File.Delete(oldPath);
                existing.ImageUrl = null;
            }

            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(fileName);
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + uniqueFileName);
                image.SaveAs(path);

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

        public IEnumerable<ProductDataEntities> Search(string query)
        {
            using (var db = new DataContext())
            {
                if (string.IsNullOrEmpty(query))
                    return new List<ProductDataEntities>();

                return db.Products
                    .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                    .Select(p => new ProductDataEntities
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl
                    })
                    .ToList();
            }
        }


    }
}
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

        public List<Product> GetAllProducts()
        {
            using (var db = new DataContext())
            {
                return db.Products.ToList();
            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new DataContext())
            {
                return db.Products.Find(id);
            }
        }

        public void AddProduct(Product product, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var uniqueFileName = Guid.NewGuid() + "_" + fileName;
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + uniqueFileName);
                image.SaveAs(path);
                product.ImageUrl = "/Content/images/" + uniqueFileName;
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product product, HttpPostedFileBase image, bool? removeImage)
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
    }
}
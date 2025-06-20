using businessLogic.Dtos.CartDtos;
using eUseControl.DataAccesss.Repositories;
using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Repositories;
using eUSeControl.BusinessLogic.Dtos.ProductDtos;
using eUSeControl.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace eUSeControl.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private bool _disposed;

        private static ProductService _instance;
        private static readonly object _lock = new object();

        private ProductService()
        {
            _repository = ProductRepository.GetInstance();
        }

        public static ProductService GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new ProductService();
                }
            }
            return _instance;
        }

        public int GetFavoriteProductsCount(int userId)
        {
            if (userId <= 0)
                return 0;

            return _repository.GetFavoriteProductsIds(userId).Count;
        }

        public async Task<ProductDto> GetByIdAsync(int productId, int userId)
        {
            var product = await _repository.GetProductByIdAsync(productId);
            return product == null
                ? null
                : MapToDto(product, userId);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(int userId)
        {
            var products = await _repository.GetAllProductsAsync();
            return products
                .Select(p => MapToDto(p, userId))
                .ToList();
        }

        public async Task<bool> UpdateProductToFavoriteAsync(int userId, int productId)
        {
            if (userId <= 0 || productId <= 0)
                return false;
            return await _repository.UpdateProductToFavoriteAsync(userId, productId);
        }

        public async Task<List<int>> GetFavoriteProductIdsAsync(int userId)
        {
            if (userId <= 0)
                return new List<int>();
            return await _repository.GetFavoriteProductIdsAsync(userId);
        }

        public async Task<List<ProductDto>> GetProductsByIdsAsync(List<int> productIds, int userId)
        {
            if (productIds == null || !productIds.Any())
                return new List<ProductDto>();
            var products = await _repository.GetProductsByIdsAsync(productIds);
            return products
                .Select(p => MapToDto(p, userId))
                .ToList();
        }

        public async Task<bool> AddProductAsync(UpsertProductDto model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                FullDescription = model.FullDescription,
                Price = model.Price,
                Sku = model.Sku,
                CategoryValue = model.Category,
                Stock = model.Stock,
                Weight = model.Weight,
                Dimensions = model.Dimensions,
                Materials = model.Materials
            };
            // Sizes
            foreach (var size in model.Sizes)
                product.ProductSizes.Add(new ProductSize { SizeValue = size });
            // Colors
            foreach (var color in model.Colors)
                product.ProductColors.Add(new ProductColor { ColorValue = color });
            // Images
            foreach (var img in model.Images)
            {
                if (img?.ContentLength > 0)
                {
                    var fn = Path.GetFileName(img.FileName);
                    var un = Guid.NewGuid() + Path.GetExtension(fn);
                    var save = HostingEnvironment.MapPath("/Content/images/" + un);
                    img.SaveAs(save);
                    product.ProductImages.Add(new ProductImage { ImageUrl = "/Content/images/" + un, SortOrder = product.ProductImages.Count + 1 });
                }
            }
            return await _repository.AddProductAsync(product);
        }

        public async Task<bool> UpdateProductAsync(UpsertProductDto model)
        {
            if (model == null || !model.Id.HasValue) return false;

            var existingDto = await _repository.GetProductByIdAsync(model.Id.Value);
            if (existingDto == null) return false;

            var updated = new Product
            {
                Id = model.Id.Value,
                Name = model.Name,
                Description = model.Description,
                FullDescription = model.FullDescription,
                Price = model.Price,
                Sku = model.Sku,
                CategoryValue = model.Category,
                Stock = model.Stock,
                Weight = model.Weight,
                Dimensions = model.Dimensions,
                Materials = model.Materials
            };

            updated.ProductSizes = model.Sizes
                .Select(s => new ProductSize { SizeValue = s })
                .ToList();

            updated.ProductColors = model.Colors
                .Select(c => new ProductColor { ColorValue = c })
                .ToList();

            updated.ProductImages = existingDto.ProductImages
                .Concat(model.Images
                    .Where(img => img?.ContentLength > 0)
                    .Select((img, i) =>
                    {
                        var fn = Path.GetFileName(img.FileName);
                        var un = Guid.NewGuid() + Path.GetExtension(fn);
                        var savePath = HostingEnvironment.MapPath("~/Content/images/" + un);
                        img.SaveAs(savePath);
                        return new ProductImage
                        {
                            ImageUrl = "/Content/images/" + un,
                            SortOrder = existingDto.ProductImages.Count + 1 + i
                        };
                    }))
                .ToList();

            return await _repository.UpdateProductAsync(updated);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            if (id <= 0) return false;
            return await _repository.DeleteProductAsync(id);
        }

        public async Task<ProductDetailsDto> GetProductDetailsByIdAsync(int productId, int userId = 0)
        {
            var x = await _repository.GetProductByIdAsync(productId);
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
                ImageUrls = x.ProductImages.OrderBy(pi => pi.SortOrder).Select(pi => pi.ImageUrl).ToList(),
                AvailableSizes = x.ProductSizes.OrderBy(ps => ps.Id).Select(ps => ps.SizeValue).ToList(),
                AvailableColors = x.ProductColors.OrderBy(pc => pc.Id).Select(pc => pc.ColorValue).ToList(),
                IsFavorite = x.ProductsToUsers.FirstOrDefault(pu => pu.UserId == userId)?.IsFavorite ?? false,
                ReviewCount = 0,
                RelatedProducts = (await _repository.GetAllProductsAsync())
                    .Where(p => p.CategoryValue == x.CategoryValue && p.Id != x.Id)
                    .Take(8)
                    .Select(p => MapToDto(p, userId))
                    .ToList(),
                UpsertCartItemDto = new UpsertCartItemDto
                {
                    ProductId = x.Id
                }
            };

            return dto;
        }

        private ProductDto MapToDto(Product x, int userId)
        {
            return new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Category = x.CategoryValue,
                ImageUrl = x.ProductImages.OrderBy(pi => pi.SortOrder).Select(pi => pi.ImageUrl).FirstOrDefault(),
                Price = x.Price,
                IsFavorite = x.ProductsToUsers.FirstOrDefault(pu => pu.UserId == userId)?.IsFavorite ?? false,
                Stock = x.Stock
            };
        }

        public async Task<UpsertProductDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);

            if (product == null)
                return new UpsertProductDto { Id = id };

            var result = new UpsertProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                FullDescription = product.FullDescription,
                Price = product.Price,
                Sku = product.Sku,
                Category = product.CategoryValue,
                Stock = product.Stock,
                Weight = product.Weight,
                Dimensions = product.Dimensions,
                Materials = product.Materials,
                Sizes = product.ProductSizes
                                      .Select(ps => ps.SizeValue)
                                      .ToList(),
                Colors = product.ProductColors
                                      .Select(pc => pc.ColorValue)
                                      .ToList(),
                ExistingImageUrls = product.ProductImages
                                 .OrderBy(pi => pi.SortOrder)
                                 .Select(pi => pi.ImageUrl)
                                 .ToList()
            };

            return result;
        }
    }
}
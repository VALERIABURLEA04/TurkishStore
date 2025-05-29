using System.Collections.Generic;
using System.Linq;
using eUseControl.Domain.Entities.Product;
using eUseControl.Web.Models.Product;

namespace eUseControl.Domain.Mappers
{
    public static class ProductMapper
    {
        // DB Entity -> Business Entity
        public static ProductEn ToBusinessEntity(Product product) => new ProductEn
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl
        };

        // Business Entity -> DB Entity
        public static Product ToDbEntity(ProductEn productEn) => new Product
        {
            Id = productEn.Id,
            Name = productEn.Name,
            Description = productEn.Description,
            Price = productEn.Price,
            ImageUrl = productEn.ImageUrl
        };

        // Business Entity -> View Model
        public static ProductModel ToViewModel(ProductEn productEn) => new ProductModel
        {
            Id = productEn.Id,
            Name = productEn.Name,
            Description = productEn.Description,
            Price = productEn.Price,
            ImageUrl = productEn.ImageUrl
        };

        // View Model -> Business Entity
        public static ProductEn ToBusinessEntity(ProductModel productModel) => new ProductEn
        {
            Id = productModel.Id,
            Name = productModel.Name,
            Description = productModel.Description,
            Price = productModel.Price,
            ImageUrl = productModel.ImageUrl
        };

        // List helpers for convenience
        public static List<ProductEn> ToBusinessEntityList(IEnumerable<Product> products) =>
            products.Select(ToBusinessEntity).ToList();

        public static List<ProductModel> ToViewModelList(IEnumerable<ProductEn> productEns) =>
            productEns.Select(ToViewModel).ToList();
    }
}

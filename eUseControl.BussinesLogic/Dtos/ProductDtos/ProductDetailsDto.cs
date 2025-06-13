using eUseControl.Domain.Enums;
using System.Collections.Generic;

namespace businessLogic.Dtos.ProductDtos
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public ProductCategoryEnum Category { get; set; }
        public int Stock { get; set; }
        public decimal? Weight { get; set; }
        public string Dimensions { get; set; }
        public string Materials { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public bool IsFavorite { get; set; }
        public int ReviewCount { get; set; }
        public List<ProductDto> RelatedProducts { get; set; } = new List<ProductDto>();
        public List<string> AvailableSizes { get; set; } = new List<string>();
        public List<string> AvailableColors { get; set; } = new List<string>();
    }
}
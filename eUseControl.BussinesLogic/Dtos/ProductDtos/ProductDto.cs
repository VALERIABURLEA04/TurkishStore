namespace businessLogic.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullDescription { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public decimal? Weight { get; set; }
        public bool? IsFavorite { get; set; }
        public string Dimensions { get; set; }
        public string Materials { get; set; }
        public int Stock { get; set; }
    }
}
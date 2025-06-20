using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace eUSeControl.BusinessLogic.Dtos.ProductDtos
{
    public class UpsertProductDto
    {
        [Display(Name = "Product ID")]
        public int? Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Name cannot exceed 150 characters.")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Display(Name = "Short Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Full Description")]
        [DataType(DataType.MultilineText)]
        public string FullDescription { get; set; }

        [Required]
        [Display(Name = "Price (MDL)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "SKU")]
        public string Sku { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Stock Quantity")]
        public int Stock { get; set; }

        [Display(Name = "Weight (kg)")]
        public decimal? Weight { get; set; }

        [Display(Name = "Dimensions")]
        public string Dimensions { get; set; }

        [Display(Name = "Materials")]
        public string Materials { get; set; }

        [Display(Name = "Available Sizes")]
        public List<string> Sizes { get; set; } = new List<string>();

        [Display(Name = "Available Colors")]
        public List<string> Colors { get; set; } = new List<string>();

        [Display(Name = "Upload Images")]
        public List<HttpPostedFileBase> Images { get; set; } = new List<HttpPostedFileBase>();

        [Display(Name = "Existing Images")]
        public List<string> ExistingImageUrls { get; set; } = new List<string>();
    }
}
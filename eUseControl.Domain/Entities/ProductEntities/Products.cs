using eUseControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.ProductEntities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string FullDescription { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sku { get; set; }

        [Column("Category")]
        public string CategoryValue { get; set; }

        [NotMapped]
        public ProductCategoryEnum Category
        {
            get => Enum.TryParse<ProductCategoryEnum>(CategoryValue, out var val) ? val : ProductCategoryEnum.None;
            set => CategoryValue = value.ToString();
        }

        public int Stock { get; set; }

        public decimal? Weight { get; set; }
        public string Dimensions { get; set; }
        public string Materials { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();

        [InverseProperty("Product")]
        public virtual ICollection<ProductToUser> ProductsToUsers { get; set; } = new HashSet<ProductToUser>();

        public virtual ICollection<ProductSize> ProductSizes { get; set; } = new HashSet<ProductSize>();
        public virtual ICollection<ProductColor> ProductColors { get; set; } = new HashSet<ProductColor>();
    }
}
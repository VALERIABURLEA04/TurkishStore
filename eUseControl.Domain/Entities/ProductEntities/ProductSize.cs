using eUseControl.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.ProductEntities
{
    [Table("ProductSizes")]
    public class ProductSize
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("Size")]
        public string SizeValue { get; set; }

        [NotMapped]
        public ProductSizeEnum Size
        {
            get => Enum.TryParse<ProductSizeEnum>(SizeValue, out var val) ? val : ProductSizeEnum.None;
            set => SizeValue = value.ToString();
        }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
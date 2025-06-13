using eUseControl.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.ProductEntities
{
    [Table("ProductColors")]
    public class ProductColor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("Color")]
        public string ColorValue { get; set; }

        [NotMapped]
        public ProductColorEnum Color
        {
            get => Enum.TryParse<ProductColorEnum>(ColorValue, out var val) ? val : ProductColorEnum.None;
            set => ColorValue = value.ToString();
        }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
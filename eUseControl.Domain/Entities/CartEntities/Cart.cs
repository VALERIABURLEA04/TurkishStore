using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Entities.UserEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.ListingEntities
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int? SizeId { get; set; }

        [ForeignKey(nameof(SizeId))]
        public virtual ProductSize Size { get; set; }

        public int? ColorId { get; set; }

        [ForeignKey(nameof(ColorId))]
        public virtual ProductColor Color { get; set; }
    }
}
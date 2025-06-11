using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.ProductEntities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public int Stock { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<ProductToUser> ProductsToUsers { get; set; } = new HashSet<ProductToUser>();
    }
}
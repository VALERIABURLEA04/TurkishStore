using eUseControl.Domain.Entities.ListingEntities;
using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Entities.UserEntities;
using System.Data.Entity;

namespace BusinessLogic.DBModel
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductToUser> ProductsToUsers { get; set; }
    }
}
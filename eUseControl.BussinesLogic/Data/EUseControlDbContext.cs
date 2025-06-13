using eUseControl.Domain.Entities.BlogEntities;
using eUseControl.Domain.Entities.ListingEntities;
using eUseControl.Domain.Entities.ProductEntities;
using eUseControl.Domain.Entities.SessionEntities;
using eUseControl.Domain.Entities.UserEntities;
using System.Data.Entity;

namespace BusinessLogic.DBModel
{
    public class EUseControlDbContext : DbContext
    {
        public EUseControlDbContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductToUser> ProductsToUsers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
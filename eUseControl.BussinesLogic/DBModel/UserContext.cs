using eUseControl.Domain.Entities.UserEntities;
using System.Data.Entity;

namespace businessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=DefaultConnection")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
}
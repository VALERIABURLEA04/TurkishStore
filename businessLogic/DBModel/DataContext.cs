using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.Admin;

namespace BusinessLogic.DBModel
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<UContactData> ContactData { get; set; }

        public DbSet<UserInfo> TBLUserInfo { get; set; }
        public DbSet<AdminData> AdminData { get; set; }
        public DbSet<Product> Products { get; set; }

    }

}

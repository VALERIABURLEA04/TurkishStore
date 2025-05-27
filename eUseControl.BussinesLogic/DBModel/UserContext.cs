using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.User;

namespace businessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() :
            base("name=DefaultConnection")
        {
        }

        public virtual DbSet<UserTable> Users { get; set; }
    }
}


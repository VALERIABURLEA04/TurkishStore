using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Session;

namespace businessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() :
            base("name=DefaultConnection")
        {
        }
        public virtual DbSet<SessionTable> Sessions { get; set; }

    }
}

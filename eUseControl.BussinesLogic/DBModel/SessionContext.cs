using eUseControl.Domain.Entities.SessionEntities;
using System.Data.Entity;

namespace businessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() :
            base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
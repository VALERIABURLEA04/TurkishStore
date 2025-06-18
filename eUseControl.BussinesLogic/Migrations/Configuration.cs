using eUSeControl.DataAccess.Data;
using System.Data.Entity.Migrations;

namespace eUSeControl.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EUseControlDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EUseControlDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
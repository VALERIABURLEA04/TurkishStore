using BusinessLogic.DBModel;
using System.Data.Entity.Migrations;

namespace businessLogic.Migrations
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
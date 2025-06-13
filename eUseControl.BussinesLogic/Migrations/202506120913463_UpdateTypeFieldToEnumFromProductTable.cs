namespace businessLogic.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateTypeFieldToEnumFromProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Category", c => c.String());
            DropColumn("dbo.Products", "Type");
        }

        public override void Down()
        {
            AddColumn("dbo.Products", "Type", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Category");
        }
    }
}
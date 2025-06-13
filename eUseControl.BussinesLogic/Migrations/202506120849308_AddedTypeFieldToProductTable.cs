namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeFieldToProductTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Type");
        }
    }
}

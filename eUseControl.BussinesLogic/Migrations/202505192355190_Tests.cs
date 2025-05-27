namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tests : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
        }
    }
}

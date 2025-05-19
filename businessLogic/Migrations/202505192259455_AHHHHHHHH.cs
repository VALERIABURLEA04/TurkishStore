namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AHHHHHHHH : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
        }
    }
}

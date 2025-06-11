namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductToUserTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductToUsers",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsFavorite = c.Boolean(nullable: false),
                        IsInCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        UserIp = c.String(maxLength: 30),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "Stock", c => c.Int(nullable: false));
            DropTable("dbo.AdminDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AdminDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.ProductToUsers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductToUsers", "UserId", "dbo.Users");
            DropIndex("dbo.ProductToUsers", new[] { "UserId" });
            DropIndex("dbo.ProductToUsers", new[] { "ProductId" });
            DropColumn("dbo.Products", "Stock");
            DropTable("dbo.Users");
            DropTable("dbo.ProductToUsers");
        }
    }
}

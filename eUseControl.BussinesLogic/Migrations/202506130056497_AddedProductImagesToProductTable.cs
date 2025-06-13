namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductImagesToProductTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColorValue = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SizeValue = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Products", "FullDescription", c => c.String());
            AddColumn("dbo.Products", "Sku", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Products", "Weight", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Products", "Dimensions", c => c.String());
            AddColumn("dbo.Products", "Materials", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Products", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            DropForeignKey("dbo.ProductColors", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductSizes", new[] { "ProductId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.ProductColors", new[] { "ProductId" });
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "Materials");
            DropColumn("dbo.Products", "Dimensions");
            DropColumn("dbo.Products", "Weight");
            DropColumn("dbo.Products", "Sku");
            DropColumn("dbo.Products", "FullDescription");
            DropTable("dbo.ProductSizes");
            DropTable("dbo.ProductImages");
            DropTable("dbo.ProductColors");
        }
    }
}

namespace eUSeControl.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BlogPostId = c.Int(nullable: false),
                        AuthorName = c.String(nullable: false, maxLength: 100),
                        CommentText = c.String(nullable: false),
                        PostedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostId, cascadeDelete: true)
                .Index(t => t.BlogPostId);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Content = c.String(nullable: false),
                        Author = c.String(nullable: false, maxLength: 100),
                        PublishDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(maxLength: 500),
                        Categories = c.String(maxLength: 200),
                        CommentsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddedDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        SizeId = c.Int(),
                        ColorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductColors", t => t.ColorId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ProductSizes", t => t.SizeId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.UserId)
                .Index(t => t.SizeId)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.ProductColors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Color = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Description = c.String(nullable: false),
                        FullDescription = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sku = c.String(nullable: false, maxLength: 50),
                        Category = c.String(),
                        Stock = c.Int(nullable: false),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        Dimensions = c.String(),
                        Materials = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Size = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
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
            
            CreateTable(
                "dbo.ContactData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Cookie = c.String(nullable: false),
                        ValidTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductColors", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductToUsers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductToUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Cart", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProductSizes", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Cart", "SizeId", "dbo.ProductSizes");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Cart", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Cart", "ColorId", "dbo.ProductColors");
            DropForeignKey("dbo.BlogComments", "BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.ProductToUsers", new[] { "UserId" });
            DropIndex("dbo.ProductToUsers", new[] { "ProductId" });
            DropIndex("dbo.ProductSizes", new[] { "ProductId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.ProductColors", new[] { "ProductId" });
            DropIndex("dbo.Cart", new[] { "ColorId" });
            DropIndex("dbo.Cart", new[] { "SizeId" });
            DropIndex("dbo.Cart", new[] { "UserId" });
            DropIndex("dbo.Cart", new[] { "ProductId" });
            DropIndex("dbo.BlogComments", new[] { "BlogPostId" });
            DropTable("dbo.Sessions");
            DropTable("dbo.ContactData");
            DropTable("dbo.Users");
            DropTable("dbo.ProductToUsers");
            DropTable("dbo.ProductSizes");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.ProductColors");
            DropTable("dbo.Cart");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogComments");
        }
    }
}

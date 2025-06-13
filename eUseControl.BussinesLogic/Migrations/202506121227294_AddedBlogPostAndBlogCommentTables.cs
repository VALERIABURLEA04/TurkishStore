namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBlogPostAndBlogCommentTables : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComments", "BlogPostId", "dbo.BlogPosts");
            DropIndex("dbo.BlogComments", new[] { "BlogPostId" });
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogComments");
        }
    }
}

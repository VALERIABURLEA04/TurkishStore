namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItemsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 100),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.TBLUserInfo");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TBLUserInfo",
                c => new
                    {
                        IdUs = c.Int(nullable: false, identity: true),
                        UsernameUS = c.String(),
                        PasswordUs = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.IdUs);
            
            DropTable("dbo.CartItems");
        }
    }
}

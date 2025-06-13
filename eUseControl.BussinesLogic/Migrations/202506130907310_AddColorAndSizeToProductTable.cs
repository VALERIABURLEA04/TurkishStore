namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorAndSizeToProductTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductColors", name: "ColorValue", newName: "Color");
            RenameColumn(table: "dbo.ProductSizes", name: "SizeValue", newName: "Size");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.ProductSizes", name: "Size", newName: "SizeValue");
            RenameColumn(table: "dbo.ProductColors", name: "Color", newName: "ColorValue");
        }
    }
}

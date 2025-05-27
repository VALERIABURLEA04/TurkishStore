namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminTableUpdate : DbMigration
    {
        public override void Up()
        {
            //RenameTable(name: "dbo.AdminDatas", newName: "AdminData");
            AlterColumn("dbo.AdminData", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AdminData", "PasswordHash", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.AdminData", "PasswordHash", c => c.String());
            AlterColumn("dbo.AdminData", "Username", c => c.String());
            //RenameTable(name: "dbo.AdminData", newName: "AdminDatas");
        }
    }
}

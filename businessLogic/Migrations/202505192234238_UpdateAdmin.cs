namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAdmin : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TBLUserInfo");
            AddColumn("dbo.TBLUserInfo", "IdUs", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TBLUserInfo", "UsernameUS", c => c.String());
            AddColumn("dbo.TBLUserInfo", "PasswordUs", c => c.String());
            AddColumn("dbo.TBLUserInfo", "Role", c => c.String());
            AddPrimaryKey("dbo.TBLUserInfo", "IdUs");
            DropColumn("dbo.TBLUserInfo", "Id");
            DropColumn("dbo.TBLUserInfo", "FirstName");
            DropColumn("dbo.TBLUserInfo", "LastName");
            DropColumn("dbo.TBLUserInfo", "Username");
            DropColumn("dbo.TBLUserInfo", "Email");
            DropColumn("dbo.TBLUserInfo", "PasswordHash");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBLUserInfo", "PasswordHash", c => c.String());
            AddColumn("dbo.TBLUserInfo", "Email", c => c.String());
            AddColumn("dbo.TBLUserInfo", "Username", c => c.String());
            AddColumn("dbo.TBLUserInfo", "LastName", c => c.String());
            AddColumn("dbo.TBLUserInfo", "FirstName", c => c.String());
            AddColumn("dbo.TBLUserInfo", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.TBLUserInfo");
            DropColumn("dbo.TBLUserInfo", "Role");
            DropColumn("dbo.TBLUserInfo", "PasswordUs");
            DropColumn("dbo.TBLUserInfo", "UsernameUS");
            DropColumn("dbo.TBLUserInfo", "IdUs");
            AddPrimaryKey("dbo.TBLUserInfo", "Id");
        }
    }
}

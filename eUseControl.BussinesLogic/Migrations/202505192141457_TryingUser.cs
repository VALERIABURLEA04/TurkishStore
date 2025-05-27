namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryingUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TBLUserInfo", newName: "UserInfoes");
            DropPrimaryKey("dbo.UserInfoes");

            // Add the new Id column with identity = true
            AddColumn("dbo.UserInfoes", "Id", c => c.Int(nullable: false, identity: true));

            // Add the other columns as usual
            AddColumn("dbo.UserInfoes", "FirstName", c => c.String());
            AddColumn("dbo.UserInfoes", "LastName", c => c.String());
            AddColumn("dbo.UserInfoes", "Username", c => c.String());
            AddColumn("dbo.UserInfoes", "Email", c => c.String());
            AddColumn("dbo.UserInfoes", "PasswordHash", c => c.String());

            // Set the primary key on the new identity column
            AddPrimaryKey("dbo.UserInfoes", "Id");

            // Now drop the old columns (including the old identity column)
            DropColumn("dbo.UserInfoes", "IdUs");
            DropColumn("dbo.UserInfoes", "UsernameUS");
            DropColumn("dbo.UserInfoes", "PasswordUs");
            DropColumn("dbo.UserInfoes", "Role");

        }
    }
}

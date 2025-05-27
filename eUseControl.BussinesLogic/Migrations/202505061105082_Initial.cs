namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactData", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ContactData", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.ContactData", "Subject", c => c.String(nullable: false));
            AlterColumn("dbo.ContactData", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactData", "Message", c => c.String());
            AlterColumn("dbo.ContactData", "Subject", c => c.String());
            AlterColumn("dbo.ContactData", "Email", c => c.String());
            AlterColumn("dbo.ContactData", "Name", c => c.String());
        }
    }
}

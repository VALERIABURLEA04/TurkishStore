namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserTables : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TBLUserInfo");
        }
    }
}

namespace businessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTable : DbMigration
    {
        public override void Up()
        {
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
            
        }
    }
}

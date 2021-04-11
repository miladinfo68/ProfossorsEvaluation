namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_ServiceUsersMapping2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceUsersMappings",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        ServiceUserId = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceUsersMappings", "User_ID", "dbo.Users");
            DropIndex("dbo.ServiceUsersMappings", new[] { "User_ID" });
            DropTable("dbo.ServiceUsersMappings");
        }
    }
}

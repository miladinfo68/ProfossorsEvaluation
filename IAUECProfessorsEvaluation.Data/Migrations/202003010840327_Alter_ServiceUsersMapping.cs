namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_ServiceUsersMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceUsersMappings", "User_ID", "dbo.Users");
            DropIndex("dbo.ServiceUsersMappings", new[] { "User_ID" });
            AddColumn("dbo.ServiceUsersMappings", "ServiceUsername", c => c.String());
            AddColumn("dbo.ServiceUsersMappings", "Username", c => c.String());
            DropColumn("dbo.ServiceUsersMappings", "ServiceUserId");
            DropColumn("dbo.ServiceUsersMappings", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceUsersMappings", "User_ID", c => c.Int());
            AddColumn("dbo.ServiceUsersMappings", "ServiceUserId", c => c.Int(nullable: false));
            DropColumn("dbo.ServiceUsersMappings", "Username");
            DropColumn("dbo.ServiceUsersMappings", "ServiceUsername");
            CreateIndex("dbo.ServiceUsersMappings", "User_ID");
            AddForeignKey("dbo.ServiceUsersMappings", "User_ID", "dbo.Users", "ID");
        }
    }
}

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReomveRoleIdInUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleUsers", "User_ID", "dbo.Users");
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropIndex("dbo.RoleUsers", new[] { "User_ID" });
            AddColumn("dbo.Users", "Role_Id", c => c.Int());
            CreateIndex("dbo.Users", "Role_Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
            DropTable("dbo.RoleUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_ID });
            
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropColumn("dbo.Users", "Role_Id");
            CreateIndex("dbo.RoleUsers", "User_ID");
            CreateIndex("dbo.RoleUsers", "Role_Id");
            AddForeignKey("dbo.RoleUsers", "User_ID", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}

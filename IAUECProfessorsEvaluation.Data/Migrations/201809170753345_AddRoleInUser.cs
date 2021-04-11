namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleInUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_Id = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.User_ID })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.User_ID);
            
            DropColumn("dbo.Users", "Role_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role_Id", c => c.Int());
            DropForeignKey("dbo.RoleUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_Id", "dbo.Roles");
            DropIndex("dbo.RoleUsers", new[] { "User_ID" });
            DropIndex("dbo.RoleUsers", new[] { "Role_Id" });
            DropTable("dbo.RoleUsers");
            CreateIndex("dbo.Users", "Role_Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
        }
    }
}

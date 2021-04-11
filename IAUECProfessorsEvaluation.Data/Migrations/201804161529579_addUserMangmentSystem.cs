namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserMangmentSystem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "User_ID", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "User_ID" });
            CreateTable(
                "dbo.Accesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role_Id = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Role_Id)
                .Index(t => t.User_ID);
            
            AddColumn("dbo.Terms", "IsCurrentTerm", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoleAccesses", "Access_Id", c => c.Int());
            CreateIndex("dbo.RoleAccesses", "Access_Id");
            AddForeignKey("dbo.RoleAccesses", "Access_Id", "dbo.Accesses", "Id");
            DropColumn("dbo.RoleAccesses", "PageId");
            DropColumn("dbo.Roles", "User_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "User_ID", c => c.Int());
            AddColumn("dbo.RoleAccesses", "PageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserRoles", "User_ID", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleAccesses", "Access_Id", "dbo.Accesses");
            DropIndex("dbo.UserRoles", new[] { "User_ID" });
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.RoleAccesses", new[] { "Access_Id" });
            DropColumn("dbo.RoleAccesses", "Access_Id");
            DropColumn("dbo.Terms", "IsCurrentTerm");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Accesses");
            CreateIndex("dbo.Roles", "User_ID");
            AddForeignKey("dbo.Roles", "User_ID", "dbo.Users", "ID");
        }
    }
}

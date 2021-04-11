namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserMangement : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleAccesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageId = c.Int(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserCount = c.Int(nullable: false),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        College_Id = c.Int(),
                        EducationalGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Colleges", t => t.College_Id)
                .ForeignKey("dbo.EducationalGroups", t => t.EducationalGroup_Id)
                .Index(t => t.College_Id)
                .Index(t => t.EducationalGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.Users", "College_Id", "dbo.Colleges");
            DropForeignKey("dbo.RoleAccesses", "Role_Id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.Users", new[] { "College_Id" });
            DropIndex("dbo.Roles", new[] { "User_ID" });
            DropIndex("dbo.RoleAccesses", new[] { "Role_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.RoleAccesses");
        }
    }
}

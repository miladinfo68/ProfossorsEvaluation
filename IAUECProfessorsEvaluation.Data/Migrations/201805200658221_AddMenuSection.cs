namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuSection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Accesses", "MenuSection_Id", c => c.Int());
            CreateIndex("dbo.Accesses", "MenuSection_Id");
            AddForeignKey("dbo.Accesses", "MenuSection_Id", "dbo.MenuSections", "Id");
            DropColumn("dbo.Roles", "UserCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "UserCount", c => c.Int(nullable: false));
            DropForeignKey("dbo.Accesses", "MenuSection_Id", "dbo.MenuSections");
            DropIndex("dbo.Accesses", new[] { "MenuSection_Id" });
            DropColumn("dbo.Accesses", "MenuSection_Id");
            DropTable("dbo.MenuSections");
        }
    }
}

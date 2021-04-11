namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMenuList : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accesses", "MenuSection_Id", "dbo.MenuSections");
            DropIndex("dbo.Accesses", new[] { "MenuSection_Id" });
            CreateTable(
                "dbo.MenuLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                        MenuSection_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuSections", t => t.MenuSection_Id)
                .Index(t => t.MenuSection_Id);
            
            AddColumn("dbo.Accesses", "MenuList_Id", c => c.Int());
            CreateIndex("dbo.Accesses", "MenuList_Id");
            AddForeignKey("dbo.Accesses", "MenuList_Id", "dbo.MenuLists", "Id");
            DropColumn("dbo.Accesses", "MenuSection_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accesses", "MenuSection_Id", c => c.Int());
            DropForeignKey("dbo.MenuLists", "MenuSection_Id", "dbo.MenuSections");
            DropForeignKey("dbo.Accesses", "MenuList_Id", "dbo.MenuLists");
            DropIndex("dbo.MenuLists", new[] { "MenuSection_Id" });
            DropIndex("dbo.Accesses", new[] { "MenuList_Id" });
            DropColumn("dbo.Accesses", "MenuList_Id");
            DropTable("dbo.MenuLists");
            CreateIndex("dbo.Accesses", "MenuSection_Id");
            AddForeignKey("dbo.Accesses", "MenuSection_Id", "dbo.MenuSections", "Id");
        }
    }
}

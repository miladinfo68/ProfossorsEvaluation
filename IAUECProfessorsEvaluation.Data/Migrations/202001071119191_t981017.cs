namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t981017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ParentRole_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Roles", "ParentRole_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Roles", new[] { "ParentRole_Id" });
            DropColumn("dbo.Roles", "ParentRole_Id");
        }
    }
}

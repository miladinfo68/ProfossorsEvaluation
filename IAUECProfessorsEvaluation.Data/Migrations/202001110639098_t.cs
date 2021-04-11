namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "ParentRole_Id", "dbo.Roles");
            DropIndex("dbo.Roles", new[] { "ParentRole_Id" });
            DropColumn("dbo.Roles", "ParentRole_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "ParentRole_Id", c => c.Int());
            CreateIndex("dbo.Roles", "ParentRole_Id");
            AddForeignKey("dbo.Roles", "ParentRole_Id", "dbo.Roles", "Id");
        }
    }
}

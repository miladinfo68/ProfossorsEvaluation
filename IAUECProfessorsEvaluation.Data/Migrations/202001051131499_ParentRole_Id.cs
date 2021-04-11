namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParentRole_Id : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "ParentRole_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.Roles", "ParentRole_Id", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roles", "ParentRole_Id1", "dbo.Roles");
            DropIndex("dbo.Roles", new[] { "ParentRole_Id1" });
            DropColumn("dbo.Roles", "ParentRole_Id1");
            DropColumn("dbo.Roles", "ParentRole_Id");
        }
    }
}

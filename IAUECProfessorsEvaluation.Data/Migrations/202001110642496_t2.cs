namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Roles", "ParentRole_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "ParentRole_Id", c => c.Int(nullable: false));
        }
    }
}

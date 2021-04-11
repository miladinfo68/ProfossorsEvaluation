namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPowerUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsPowerUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsPowerUser");
        }
    }
}

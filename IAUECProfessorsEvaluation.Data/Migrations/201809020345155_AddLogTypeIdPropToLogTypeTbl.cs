namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogTypeIdPropToLogTypeTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogTypes", "LogTypeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogTypes", "LogTypeID");
        }
    }
}

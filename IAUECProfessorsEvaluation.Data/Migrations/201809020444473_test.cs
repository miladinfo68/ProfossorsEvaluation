namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "LastRunDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Schedules", "NextRunDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "NextRunDate");
            DropColumn("dbo.Schedules", "LastRunDate");
        }
    }
}

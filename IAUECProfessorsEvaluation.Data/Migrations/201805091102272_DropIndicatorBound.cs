namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropIndicatorBound : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Scores", "UpperBound");
            DropColumn("dbo.Scores", "LowerBound");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Scores", "LowerBound", c => c.Single());
            AddColumn("dbo.Scores", "UpperBound", c => c.Single());
        }
    }
}

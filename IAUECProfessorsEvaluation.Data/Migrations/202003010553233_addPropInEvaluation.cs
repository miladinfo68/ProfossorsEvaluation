namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPropInEvaluation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Evaluation.Question", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("Evaluation.Question", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Evaluation.Question", "EndDate");
            DropColumn("Evaluation.Question", "StartDate");
        }
    }
}

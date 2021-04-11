namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEvaluationPropType : DbMigration
    {
        public override void Up()
        {
            AddColumn("Evaluation.Comment", "StudentCode", c => c.String());
            DropColumn("Evaluation.Comment", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("Evaluation.Comment", "StudentId", c => c.Int(nullable: false));
            DropColumn("Evaluation.Comment", "StudentCode");
        }
    }
}

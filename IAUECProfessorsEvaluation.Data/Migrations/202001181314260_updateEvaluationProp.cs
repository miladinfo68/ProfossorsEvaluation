namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateEvaluationProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("Evaluation.Answer", "Text", c => c.String());
            DropColumn("Evaluation.Answer", "Title");
        }
        
        public override void Down()
        {
            AddColumn("Evaluation.Answer", "Title", c => c.String());
            DropColumn("Evaluation.Answer", "Text");
        }
    }
}

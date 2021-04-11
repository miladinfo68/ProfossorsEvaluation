namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableAndPropInEvaluation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Evaluation.ChartType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Evaluation.QuestionAnswerComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TermId = c.Int(nullable: false),
                        PersonalCode = c.String(),
                        AnswerComment = c.String(),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Evaluation.Answer", "Score", c => c.Int(nullable: false));
            AddColumn("Evaluation.Answer", "Order", c => c.Int(nullable: false));
            AddColumn("Evaluation.Question", "IsPossibilityToInsertComment", c => c.Boolean(nullable: false));
            AddColumn("Evaluation.Question", "QuestionType", c => c.Int(nullable: false));
            AddColumn("Evaluation.Question", "EvaluationChartTypeId", c => c.Int(nullable: false));
            AddColumn("Evaluation.QuestionAnswer", "TermId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Evaluation.QuestionAnswer", "TermId");
            DropColumn("Evaluation.Question", "EvaluationChartTypeId");
            DropColumn("Evaluation.Question", "QuestionType");
            DropColumn("Evaluation.Question", "IsPossibilityToInsertComment");
            DropColumn("Evaluation.Answer", "Order");
            DropColumn("Evaluation.Answer", "Score");
            DropTable("Evaluation.QuestionAnswerComment");
            DropTable("Evaluation.ChartType");
        }
    }
}

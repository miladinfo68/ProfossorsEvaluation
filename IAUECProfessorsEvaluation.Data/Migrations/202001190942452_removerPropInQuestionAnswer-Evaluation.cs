namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerPropInQuestionAnswerEvaluation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Evaluation.QuestionAnswer", "AnswerId", "Evaluation.Answer");
            DropForeignKey("Evaluation.QuestionAnswer", "QuestionId", "Evaluation.Question");
            DropForeignKey("Evaluation.QuestionAnswer", "SubQuestionId", "Evaluation.SubQuestion");
            DropIndex("Evaluation.QuestionAnswer", new[] { "QuestionId" });
            DropIndex("Evaluation.QuestionAnswer", new[] { "SubQuestionId" });
            DropIndex("Evaluation.QuestionAnswer", new[] { "AnswerId" });
        }
        
        public override void Down()
        {
            CreateIndex("Evaluation.QuestionAnswer", "AnswerId");
            CreateIndex("Evaluation.QuestionAnswer", "SubQuestionId");
            CreateIndex("Evaluation.QuestionAnswer", "QuestionId");
            AddForeignKey("Evaluation.QuestionAnswer", "SubQuestionId", "Evaluation.SubQuestion", "Id");
            AddForeignKey("Evaluation.QuestionAnswer", "QuestionId", "Evaluation.Question", "Id", cascadeDelete: true);
            AddForeignKey("Evaluation.QuestionAnswer", "AnswerId", "Evaluation.Answer", "Id", cascadeDelete: true);
        }
    }
}

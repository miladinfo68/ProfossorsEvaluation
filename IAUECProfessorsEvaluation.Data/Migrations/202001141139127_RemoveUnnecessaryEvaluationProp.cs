namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnnecessaryEvaluationProp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Evaluation.Answer", "QuestionId", "Evaluation.Question");
            DropIndex("Evaluation.Answer", new[] { "QuestionId" });
            RenameColumn(table: "Evaluation.Answer", name: "QuestionId", newName: "Question_Id");
            AlterColumn("Evaluation.Answer", "Question_Id", c => c.Int());
            CreateIndex("Evaluation.Answer", "Question_Id");
            AddForeignKey("Evaluation.Answer", "Question_Id", "Evaluation.Question", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("Evaluation.Answer", "Question_Id", "Evaluation.Question");
            DropIndex("Evaluation.Answer", new[] { "Question_Id" });
            AlterColumn("Evaluation.Answer", "Question_Id", c => c.Int(nullable: false));
            RenameColumn(table: "Evaluation.Answer", name: "Question_Id", newName: "QuestionId");
            CreateIndex("Evaluation.Answer", "QuestionId");
            AddForeignKey("Evaluation.Answer", "QuestionId", "Evaluation.Question", "Id", cascadeDelete: true);
        }
    }
}

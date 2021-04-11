namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPropInEvaluationModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Evaluation.QuestionAnswer", "Question_Id", "Evaluation.Question");
            DropIndex("Evaluation.QuestionAnswer", new[] { "Question_Id" });
            RenameColumn(table: "Evaluation.QuestionAnswer", name: "Question_Id", newName: "QuestionId");
            AlterColumn("Evaluation.QuestionAnswer", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("Evaluation.QuestionAnswer", "QuestionId");
            AddForeignKey("Evaluation.QuestionAnswer", "QuestionId", "Evaluation.Question", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Evaluation.QuestionAnswer", "QuestionId", "Evaluation.Question");
            DropIndex("Evaluation.QuestionAnswer", new[] { "QuestionId" });
            AlterColumn("Evaluation.QuestionAnswer", "QuestionId", c => c.Int());
            RenameColumn(table: "Evaluation.QuestionAnswer", name: "QuestionId", newName: "Question_Id");
            CreateIndex("Evaluation.QuestionAnswer", "Question_Id");
            AddForeignKey("Evaluation.QuestionAnswer", "Question_Id", "Evaluation.Question", "Id");
        }
    }
}

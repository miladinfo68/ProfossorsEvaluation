namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfessorEvaluationProp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Evaluation.SubQuestion", "QuestionId", "Evaluation.Question");
            DropIndex("Evaluation.SubQuestion", new[] { "QuestionId" });
            CreateTable(
                "Evaluation.Type",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Evaluation.Question", "EvaluationType_Id", c => c.Int());
            AddColumn("Evaluation.QuestionAnswer", "StudentCode", c => c.String());
            AddColumn("Evaluation.QuestionAnswer", "ProfessorId", c => c.Int());
            AddColumn("Evaluation.QuestionAnswer", "ClassId", c => c.Int());
            CreateIndex("Evaluation.Question", "EvaluationType_Id");
            AddForeignKey("Evaluation.Question", "EvaluationType_Id", "Evaluation.Type", "Id");
            DropColumn("Evaluation.QuestionAnswer", "StudentId");
            DropTable("Evaluation.SubQuestion");
        }
        
        public override void Down()
        {
            CreateTable(
                "Evaluation.SubQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Evaluation.QuestionAnswer", "StudentId", c => c.Int(nullable: false));
            DropForeignKey("Evaluation.Question", "EvaluationType_Id", "Evaluation.Type");
            DropIndex("Evaluation.Question", new[] { "EvaluationType_Id" });
            DropColumn("Evaluation.QuestionAnswer", "ClassId");
            DropColumn("Evaluation.QuestionAnswer", "ProfessorId");
            DropColumn("Evaluation.QuestionAnswer", "StudentCode");
            DropColumn("Evaluation.Question", "EvaluationType_Id");
            DropTable("Evaluation.Type");
            CreateIndex("Evaluation.SubQuestion", "QuestionId");
            AddForeignKey("Evaluation.SubQuestion", "QuestionId", "Evaluation.Question", "Id", cascadeDelete: true);
        }
    }
}

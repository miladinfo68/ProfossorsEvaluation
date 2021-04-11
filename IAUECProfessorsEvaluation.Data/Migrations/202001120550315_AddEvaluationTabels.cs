namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEvaluationTabels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Evaluation.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        QuestionId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evaluation.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "Evaluation.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Description = c.String(),
                        IsLastQuestion = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.Term_Id);
            
            CreateTable(
                "Evaluation.SubQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Text = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evaluation.Question", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "Evaluation.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Term = c.String(),
                        StudentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Evaluation.QuestionAnswer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubQuestionId = c.Int(),
                        AnswerId = c.Int(nullable: false),
                        Term = c.String(),
                        StudentId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Evaluation.Answer", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("Evaluation.Question", t => t.Question_Id)
                .ForeignKey("Evaluation.SubQuestion", t => t.SubQuestionId)
                .Index(t => t.SubQuestionId)
                .Index(t => t.AnswerId)
                .Index(t => t.Question_Id);
            
            DropColumn("dbo.Terms", "ExamStartDate");
            DropColumn("dbo.Terms", "ExamEndDate");
            DropColumn("dbo.EducationalGroups", "OtherPresenceTime");
            DropColumn("dbo.EducationalClasses", "ReceiveExamPaperDate");
            DropColumn("dbo.EducationalClasses", "AggregationExamPaperDate");
            DropColumn("dbo.EducationalClasses", "LessonPlanSendDate");
            DropColumn("dbo.EducationalClasses", "ContentType");
            DropColumn("dbo.EducationalClasses", "HoldingType");
            DropColumn("dbo.Professors", "DeputyComments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Professors", "DeputyComments", c => c.Int());
            AddColumn("dbo.EducationalClasses", "HoldingType", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.EducationalClasses", "ContentType", c => c.Int());
            AddColumn("dbo.EducationalClasses", "LessonPlanSendDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "AggregationExamPaperDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "ReceiveExamPaperDate", c => c.DateTime());
            AddColumn("dbo.EducationalGroups", "OtherPresenceTime", c => c.Int());
            AddColumn("dbo.Terms", "ExamEndDate", c => c.DateTime());
            AddColumn("dbo.Terms", "ExamStartDate", c => c.DateTime());
            DropForeignKey("Evaluation.QuestionAnswer", "SubQuestionId", "Evaluation.SubQuestion");
            DropForeignKey("Evaluation.QuestionAnswer", "Question_Id", "Evaluation.Question");
            DropForeignKey("Evaluation.QuestionAnswer", "AnswerId", "Evaluation.Answer");
            DropForeignKey("Evaluation.Answer", "QuestionId", "Evaluation.Question");
            DropForeignKey("Evaluation.Question", "Term_Id", "dbo.Terms");
            DropForeignKey("Evaluation.SubQuestion", "QuestionId", "Evaluation.Question");
            DropIndex("Evaluation.QuestionAnswer", new[] { "Question_Id" });
            DropIndex("Evaluation.QuestionAnswer", new[] { "AnswerId" });
            DropIndex("Evaluation.QuestionAnswer", new[] { "SubQuestionId" });
            DropIndex("Evaluation.SubQuestion", new[] { "QuestionId" });
            DropIndex("Evaluation.Question", new[] { "Term_Id" });
            DropIndex("Evaluation.Answer", new[] { "QuestionId" });
            DropTable("Evaluation.QuestionAnswer");
            DropTable("Evaluation.Comment");
            DropTable("Evaluation.SubQuestion");
            DropTable("Evaluation.Question");
            DropTable("Evaluation.Answer");
        }
    }
}

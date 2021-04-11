namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCoreModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProfessorColleges", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.ProfessorColleges", "College_Id", "dbo.Colleges");
            DropForeignKey("dbo.Colleges", "EducationalClass_Id", "dbo.EducationalClasses");
            DropForeignKey("dbo.EducationalGroups", "EducationalClass_Id", "dbo.EducationalClasses");
            DropForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropIndex("dbo.Colleges", new[] { "EducationalClass_Id" });
            DropIndex("dbo.EducationalGroups", new[] { "EducationalClass_Id" });
            DropIndex("dbo.ProfessorColleges", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorColleges", new[] { "College_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "EducationalGroup_Id" });
            DropPrimaryKey("dbo.ProfessorEducationalGroups");
            CreateTable(
                "dbo.EducationalClassEducationalGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalClass_Id = c.Int(),
                        EducationalGroup_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationalClasses", t => t.EducationalClass_Id)
                .ForeignKey("dbo.EducationalGroups", t => t.EducationalGroup_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.EducationalClass_Id)
                .Index(t => t.EducationalGroup_Id)
                .Index(t => t.Term_Id);
            
            AddColumn("dbo.EducationalGroups", "OnlinePresenceTime", c => c.Int());
            AddColumn("dbo.EducationalGroups", "PhysicalPresenceTime", c => c.Int());
            AddColumn("dbo.EducationalGroups", "AverageStudentGrades", c => c.Int());
            AddColumn("dbo.EducationalGroups", "ExpelledStudentsPercentage", c => c.Int());
            AddColumn("dbo.EducationalGroups", "StudentCancellationPercentage", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TeacherToStudentRatio", c => c.Int());
            AddColumn("dbo.EducationalGroups", "ApproveProposalsPercentage", c => c.Int());
            AddColumn("dbo.EducationalGroups", "InTimePresentCurriculum", c => c.String());
            AddColumn("dbo.EducationalGroups", "GroupManger_Id", c => c.Int());
            AddColumn("dbo.EducationalGroups", "Term_Id", c => c.Int());
            AddColumn("dbo.Professors", "ScientificRank", c => c.Int());
            AddColumn("dbo.Professors", "TeachingExperience", c => c.Int());
            AddColumn("dbo.Professors", "AcademicDegree", c => c.Int());
            AddColumn("dbo.Professors", "EducationPlaceUniversityRank", c => c.Int());
            AddColumn("dbo.Professors", "UniversityWorkPlace", c => c.Int());
            AddColumn("dbo.Professors", "InPersonSession", c => c.Int());
            AddColumn("dbo.Professors", "OnlineSession", c => c.Int());
            AddColumn("dbo.Professors", "OthersSession", c => c.Int());
            AddColumn("dbo.Professors", "Description", c => c.String());
            AddColumn("dbo.Professors", "EntryAndExitStatus", c => c.Int());
            AddColumn("dbo.Professors", "ScoresAnnounceTimely", c => c.Int());
            AddColumn("dbo.Professors", "ExamQuestionsProvideTimely", c => c.Int());
            AddColumn("dbo.Professors", "MasterAccessStatus", c => c.Int());
            AddColumn("dbo.Professors", "GroupMangerComments", c => c.Int());
            AddColumn("dbo.Professors", "Term_Id", c => c.Int());
            AddColumn("dbo.Professors", "EducationalGroup_Id", c => c.Int());
            AddColumn("dbo.EducationalClasses", "ProfessorDelay", c => c.Int());
            AddColumn("dbo.EducationalClasses", "ProfessorEarlier", c => c.Int());
            AddColumn("dbo.EducationalClasses", "HoldingExamDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "DeclaringScoreDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "LoadingQuestionDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "EvaluationScore", c => c.Int());
            AddColumn("dbo.EducationalClasses", "OnlineHeldingCount", c => c.Int());
            AddColumn("dbo.EducationalClasses", "PersentHeldingCount", c => c.Int());
            AddColumn("dbo.EducationalClasses", "OthersHeldingCount", c => c.Int());
            AddColumn("dbo.EducationalClasses", "Term_Id", c => c.Int());
            AddColumn("dbo.ProfessorEducationalGroups", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ProfessorEducationalGroups", "Name", c => c.String());
            AddColumn("dbo.ProfessorEducationalGroups", "CreationDate", c => c.DateTime());
            AddColumn("dbo.ProfessorEducationalGroups", "LastModifiedDate", c => c.DateTime());
            AddColumn("dbo.ProfessorEducationalGroups", "IsActive", c => c.Boolean());
            AddColumn("dbo.ProfessorEducationalGroups", "Term_Id", c => c.Int());
            AlterColumn("dbo.Colleges", "IsActive", c => c.Boolean());
            AlterColumn("dbo.Scores", "IsActive", c => c.Boolean());
            AlterColumn("dbo.Indicators", "IsActive", c => c.Boolean());
            AlterColumn("dbo.ObjectTypes", "IsActive", c => c.Boolean());
            AlterColumn("dbo.Ratios", "Point", c => c.Int());
            AlterColumn("dbo.Ratios", "IsActive", c => c.Boolean());
            AlterColumn("dbo.Terms", "IsActive", c => c.Boolean());
            AlterColumn("dbo.EducationalGroups", "IsActive", c => c.Boolean());
            AlterColumn("dbo.EducationalGroupScores", "CurrentScore", c => c.Int());
            AlterColumn("dbo.Professors", "Gender", c => c.Boolean());
            AlterColumn("dbo.Professors", "IsActive", c => c.Boolean());
            AlterColumn("dbo.EducationalClasses", "IsActive", c => c.Boolean());
            AlterColumn("dbo.ProfessorScores", "CurrentScore", c => c.Int());
            AlterColumn("dbo.ProfessorEducationalGroups", "Professor_Id", c => c.Int());
            AlterColumn("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", c => c.Int());
            AddPrimaryKey("dbo.ProfessorEducationalGroups", "Id");
            CreateIndex("dbo.EducationalGroups", "GroupManger_Id");
            CreateIndex("dbo.EducationalGroups", "Term_Id");
            CreateIndex("dbo.EducationalClasses", "Term_Id");
            CreateIndex("dbo.Professors", "Term_Id");
            CreateIndex("dbo.Professors", "EducationalGroup_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "EducationalGroup_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "Professor_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "Term_Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.Professors", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.EducationalClasses", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.EducationalGroups", "GroupManger_Id", "dbo.Professors", "Id");
            AddForeignKey("dbo.Professors", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            AddForeignKey("dbo.EducationalGroups", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors", "Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            DropColumn("dbo.Colleges", "EducationalClass_Id");
            DropColumn("dbo.EducationalGroups", "EducationalClass_Id");
            DropTable("dbo.ProfessorColleges");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProfessorColleges",
                c => new
                    {
                        Professor_Id = c.Int(nullable: false),
                        College_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.College_Id });
            
            AddColumn("dbo.EducationalGroups", "EducationalClass_Id", c => c.Int());
            AddColumn("dbo.Colleges", "EducationalClass_Id", c => c.Int());
            DropForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.EducationalGroups", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.Professors", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.EducationalGroups", "GroupManger_Id", "dbo.Professors");
            DropForeignKey("dbo.EducationalClassEducationalGroups", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.EducationalClassEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.EducationalClasses", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.Professors", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.ProfessorEducationalGroups", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.EducationalClassEducationalGroups", "EducationalClass_Id", "dbo.EducationalClasses");
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "Term_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.Professors", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.Professors", new[] { "Term_Id" });
            DropIndex("dbo.EducationalClasses", new[] { "Term_Id" });
            DropIndex("dbo.EducationalClassEducationalGroups", new[] { "Term_Id" });
            DropIndex("dbo.EducationalClassEducationalGroups", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.EducationalClassEducationalGroups", new[] { "EducationalClass_Id" });
            DropIndex("dbo.EducationalGroups", new[] { "Term_Id" });
            DropIndex("dbo.EducationalGroups", new[] { "GroupManger_Id" });
            DropPrimaryKey("dbo.ProfessorEducationalGroups");
            AlterColumn("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ProfessorEducationalGroups", "Professor_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ProfessorScores", "CurrentScore", c => c.Int(nullable: false));
            AlterColumn("dbo.EducationalClasses", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Professors", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Professors", "Gender", c => c.Boolean(nullable: false));
            AlterColumn("dbo.EducationalGroupScores", "CurrentScore", c => c.Int(nullable: false));
            AlterColumn("dbo.EducationalGroups", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Terms", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Ratios", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Ratios", "Point", c => c.Int(nullable: false));
            AlterColumn("dbo.ObjectTypes", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Indicators", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Scores", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Colleges", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProfessorEducationalGroups", "Term_Id");
            DropColumn("dbo.ProfessorEducationalGroups", "IsActive");
            DropColumn("dbo.ProfessorEducationalGroups", "LastModifiedDate");
            DropColumn("dbo.ProfessorEducationalGroups", "CreationDate");
            DropColumn("dbo.ProfessorEducationalGroups", "Name");
            DropColumn("dbo.ProfessorEducationalGroups", "Id");
            DropColumn("dbo.EducationalClasses", "Term_Id");
            DropColumn("dbo.EducationalClasses", "OthersHeldingCount");
            DropColumn("dbo.EducationalClasses", "PersentHeldingCount");
            DropColumn("dbo.EducationalClasses", "OnlineHeldingCount");
            DropColumn("dbo.EducationalClasses", "EvaluationScore");
            DropColumn("dbo.EducationalClasses", "LoadingQuestionDate");
            DropColumn("dbo.EducationalClasses", "DeclaringScoreDate");
            DropColumn("dbo.EducationalClasses", "HoldingExamDate");
            DropColumn("dbo.EducationalClasses", "ProfessorEarlier");
            DropColumn("dbo.EducationalClasses", "ProfessorDelay");
            DropColumn("dbo.Professors", "EducationalGroup_Id");
            DropColumn("dbo.Professors", "Term_Id");
            DropColumn("dbo.Professors", "GroupMangerComments");
            DropColumn("dbo.Professors", "MasterAccessStatus");
            DropColumn("dbo.Professors", "ExamQuestionsProvideTimely");
            DropColumn("dbo.Professors", "ScoresAnnounceTimely");
            DropColumn("dbo.Professors", "EntryAndExitStatus");
            DropColumn("dbo.Professors", "Description");
            DropColumn("dbo.Professors", "OthersSession");
            DropColumn("dbo.Professors", "OnlineSession");
            DropColumn("dbo.Professors", "InPersonSession");
            DropColumn("dbo.Professors", "UniversityWorkPlace");
            DropColumn("dbo.Professors", "EducationPlaceUniversityRank");
            DropColumn("dbo.Professors", "AcademicDegree");
            DropColumn("dbo.Professors", "TeachingExperience");
            DropColumn("dbo.Professors", "ScientificRank");
            DropColumn("dbo.EducationalGroups", "Term_Id");
            DropColumn("dbo.EducationalGroups", "GroupManger_Id");
            DropColumn("dbo.EducationalGroups", "InTimePresentCurriculum");
            DropColumn("dbo.EducationalGroups", "ApproveProposalsPercentage");
            DropColumn("dbo.EducationalGroups", "TeacherToStudentRatio");
            DropColumn("dbo.EducationalGroups", "StudentCancellationPercentage");
            DropColumn("dbo.EducationalGroups", "ExpelledStudentsPercentage");
            DropColumn("dbo.EducationalGroups", "AverageStudentGrades");
            DropColumn("dbo.EducationalGroups", "PhysicalPresenceTime");
            DropColumn("dbo.EducationalGroups", "OnlinePresenceTime");
            DropTable("dbo.EducationalClassEducationalGroups");
            AddPrimaryKey("dbo.ProfessorEducationalGroups", new[] { "Professor_Id", "EducationalGroup_Id" });
            CreateIndex("dbo.ProfessorEducationalGroups", "EducationalGroup_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "Professor_Id");
            CreateIndex("dbo.ProfessorColleges", "College_Id");
            CreateIndex("dbo.ProfessorColleges", "Professor_Id");
            CreateIndex("dbo.EducationalGroups", "EducationalClass_Id");
            CreateIndex("dbo.Colleges", "EducationalClass_Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EducationalGroups", "EducationalClass_Id", "dbo.EducationalClasses", "Id");
            AddForeignKey("dbo.Colleges", "EducationalClass_Id", "dbo.EducationalClasses", "Id");
            AddForeignKey("dbo.ProfessorColleges", "College_Id", "dbo.Colleges", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProfessorColleges", "Professor_Id", "dbo.Professors", "Id", cascadeDelete: true);
        }
    }
}

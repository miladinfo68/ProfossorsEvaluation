namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colleges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CollegeCode = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        EducationalClass_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationalClasses", t => t.EducationalClass_Id)
                .Index(t => new { t.Name, t.CollegeCode })
                .Index(t => t.EducationalClass_Id);
            
            CreateTable(
                "dbo.CollegeScores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CurrentScore = c.Int(nullable: false),
                        College_Id = c.Int(),
                        Score_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colleges", t => t.College_Id)
                .ForeignKey("dbo.Scores", t => t.Score_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.College_Id)
                .Index(t => t.Score_Id)
                .Index(t => t.Term_Id);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MaxValue = c.Single(),
                        MinValue = c.Single(),
                        UpperBound = c.Single(),
                        LowerBound = c.Single(),
                        Point = c.Single(nullable: false),
                        Name = c.String(maxLength: 300),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Indicator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Indicators", t => t.Indicator_Id)
                .Index(t => t.Indicator_Id);
            
            CreateTable(
                "dbo.Indicators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        ObjectType_Id = c.Int(),
                        Ratio_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ObjectTypes", t => t.ObjectType_Id)
                .ForeignKey("dbo.Ratios", t => t.Ratio_Id)
                .Index(t => t.Name)
                .Index(t => t.ObjectType_Id)
                .Index(t => t.Ratio_Id);
            
            CreateTable(
                "dbo.ObjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Ratios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Point = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Terms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TermCode = c.String(nullable: false),
                        Name = c.String(maxLength: 200),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.EducationalGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CollegeCode = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        College_Id = c.Int(),
                        EducationalClass_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colleges", t => t.College_Id)
                .ForeignKey("dbo.EducationalClasses", t => t.EducationalClass_Id)
                .Index(t => new { t.CollegeCode, t.Name })
                .Index(t => t.College_Id)
                .Index(t => t.EducationalClass_Id);
            
            CreateTable(
                "dbo.EducationalGroupScores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CurrentScore = c.Int(nullable: false),
                        EducationalGroup_Id = c.Int(),
                        Score_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationalGroups", t => t.EducationalGroup_Id)
                .ForeignKey("dbo.Scores", t => t.Score_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.EducationalGroup_Id)
                .Index(t => t.Score_Id)
                .Index(t => t.Term_Id);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProfessorCode = c.Int(nullable: false),
                        Family = c.String(),
                        Name = c.String(nullable: false, maxLength: 200),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ProfessorCode);
            
            CreateTable(
                "dbo.EducationalClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeClass = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Professor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professors", t => t.Professor_Id)
                .Index(t => new { t.CodeClass, t.Name })
                .Index(t => t.Professor_Id);
            
            CreateTable(
                "dbo.ProfessorScores",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CurrentScore = c.Int(nullable: false),
                        Professor_Id = c.Int(),
                        Score_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Professors", t => t.Professor_Id)
                .ForeignKey("dbo.Scores", t => t.Score_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.Professor_Id)
                .Index(t => t.Score_Id)
                .Index(t => t.Term_Id);
            
            CreateTable(
                "dbo.ProfessorColleges",
                c => new
                    {
                        Professor_Id = c.Int(nullable: false),
                        College_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.College_Id })
                .ForeignKey("dbo.Professors", t => t.Professor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Colleges", t => t.College_Id, cascadeDelete: true)
                .Index(t => t.Professor_Id)
                .Index(t => t.College_Id);
            
            CreateTable(
                "dbo.ProfessorEducationalGroups",
                c => new
                    {
                        Professor_Id = c.Int(nullable: false),
                        EducationalGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.EducationalGroup_Id })
                .ForeignKey("dbo.Professors", t => t.Professor_Id, cascadeDelete: true)
                .ForeignKey("dbo.EducationalGroups", t => t.EducationalGroup_Id, cascadeDelete: true)
                .Index(t => t.Professor_Id)
                .Index(t => t.EducationalGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfessorScores", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.ProfessorScores", "Score_Id", "dbo.Scores");
            DropForeignKey("dbo.ProfessorScores", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.EducationalClasses", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.EducationalGroups", "EducationalClass_Id", "dbo.EducationalClasses");
            DropForeignKey("dbo.Colleges", "EducationalClass_Id", "dbo.EducationalClasses");
            DropForeignKey("dbo.ProfessorColleges", "College_Id", "dbo.Colleges");
            DropForeignKey("dbo.ProfessorColleges", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.EducationalGroupScores", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.EducationalGroupScores", "Score_Id", "dbo.Scores");
            DropForeignKey("dbo.EducationalGroupScores", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.EducationalGroups", "College_Id", "dbo.Colleges");
            DropForeignKey("dbo.CollegeScores", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.CollegeScores", "Score_Id", "dbo.Scores");
            DropForeignKey("dbo.Scores", "Indicator_Id", "dbo.Indicators");
            DropForeignKey("dbo.Indicators", "Ratio_Id", "dbo.Ratios");
            DropForeignKey("dbo.Indicators", "ObjectType_Id", "dbo.ObjectTypes");
            DropForeignKey("dbo.CollegeScores", "College_Id", "dbo.Colleges");
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorColleges", new[] { "College_Id" });
            DropIndex("dbo.ProfessorColleges", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorScores", new[] { "Term_Id" });
            DropIndex("dbo.ProfessorScores", new[] { "Score_Id" });
            DropIndex("dbo.ProfessorScores", new[] { "Professor_Id" });
            DropIndex("dbo.EducationalClasses", new[] { "Professor_Id" });
            DropIndex("dbo.EducationalClasses", new[] { "CodeClass", "Name" });
            DropIndex("dbo.Professors", new[] { "ProfessorCode" });
            DropIndex("dbo.EducationalGroupScores", new[] { "Term_Id" });
            DropIndex("dbo.EducationalGroupScores", new[] { "Score_Id" });
            DropIndex("dbo.EducationalGroupScores", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.EducationalGroups", new[] { "EducationalClass_Id" });
            DropIndex("dbo.EducationalGroups", new[] { "College_Id" });
            DropIndex("dbo.EducationalGroups", new[] { "CollegeCode", "Name" });
            DropIndex("dbo.Terms", new[] { "Name" });
            DropIndex("dbo.Ratios", new[] { "Name" });
            DropIndex("dbo.ObjectTypes", new[] { "Name" });
            DropIndex("dbo.Indicators", new[] { "Ratio_Id" });
            DropIndex("dbo.Indicators", new[] { "ObjectType_Id" });
            DropIndex("dbo.Indicators", new[] { "Name" });
            DropIndex("dbo.Scores", new[] { "Indicator_Id" });
            DropIndex("dbo.CollegeScores", new[] { "Term_Id" });
            DropIndex("dbo.CollegeScores", new[] { "Score_Id" });
            DropIndex("dbo.CollegeScores", new[] { "College_Id" });
            DropIndex("dbo.Colleges", new[] { "EducationalClass_Id" });
            DropIndex("dbo.Colleges", new[] { "Name", "CollegeCode" });
            DropTable("dbo.ProfessorEducationalGroups");
            DropTable("dbo.ProfessorColleges");
            DropTable("dbo.ProfessorScores");
            DropTable("dbo.EducationalClasses");
            DropTable("dbo.Professors");
            DropTable("dbo.EducationalGroupScores");
            DropTable("dbo.EducationalGroups");
            DropTable("dbo.Terms");
            DropTable("dbo.Ratios");
            DropTable("dbo.ObjectTypes");
            DropTable("dbo.Indicators");
            DropTable("dbo.Scores");
            DropTable("dbo.CollegeScores");
            DropTable("dbo.Colleges");
        }
    }
}

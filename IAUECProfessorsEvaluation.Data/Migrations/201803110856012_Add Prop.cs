namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proposals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalGroup_Id = c.Int(),
                        Student_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationalGroups", t => t.EducationalGroup_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.EducationalGroup_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Term_Id);
            
            AddColumn("dbo.Professors", "Status", c => c.Int());
            AddColumn("dbo.Professors", "UniversityStudyPlace", c => c.Int());
            AddColumn("dbo.Professors", "ProfessorAccessStatus", c => c.Int());
            AddColumn("dbo.StudentEducationalClasses", "ProfessorEvaluationScore", c => c.Int());
            AddColumn("dbo.Students", "EducationalLevel", c => c.Int());
            DropColumn("dbo.EducationalClassEducationalGroups", "Name");
            DropColumn("dbo.Professors", "MasterAccessStatus");
            DropColumn("dbo.ProfessorEducationalGroups", "Name");
            DropColumn("dbo.StudentEducationalClasses", "Name");
            DropColumn("dbo.Mappings", "Name");
            DropColumn("dbo.UniversityLevelMappings", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UniversityLevelMappings", "Name", c => c.String());
            AddColumn("dbo.Mappings", "Name", c => c.String());
            AddColumn("dbo.StudentEducationalClasses", "Name", c => c.String());
            AddColumn("dbo.ProfessorEducationalGroups", "Name", c => c.String());
            AddColumn("dbo.Professors", "MasterAccessStatus", c => c.Int());
            AddColumn("dbo.EducationalClassEducationalGroups", "Name", c => c.String());
            DropForeignKey("dbo.Proposals", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.Proposals", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Proposals", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropIndex("dbo.Proposals", new[] { "Term_Id" });
            DropIndex("dbo.Proposals", new[] { "Student_Id" });
            DropIndex("dbo.Proposals", new[] { "EducationalGroup_Id" });
            DropColumn("dbo.Students", "EducationalLevel");
            DropColumn("dbo.StudentEducationalClasses", "ProfessorEvaluationScore");
            DropColumn("dbo.Professors", "ProfessorAccessStatus");
            DropColumn("dbo.Professors", "UniversityStudyPlace");
            DropColumn("dbo.Professors", "Status");
            DropTable("dbo.Proposals");
        }
    }
}

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EducationalClassEducationalGroups", "EducationalClass_Id", "dbo.EducationalClasses");
            DropForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors");
            DropForeignKey("dbo.ProfessorEducationalGroups", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.Students", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.StudentEducationalClasses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.EducationalClassEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.EducationalClassEducationalGroups", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.Professors", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.Proposals", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.Proposals", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Proposals", "Term_Id", "dbo.Terms");
            DropIndex("dbo.EducationalClassEducationalGroups", new[] { "EducationalClass_Id" });
            DropIndex("dbo.EducationalClassEducationalGroups", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.EducationalClassEducationalGroups", new[] { "Term_Id" });
            DropIndex("dbo.Professors", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "Professor_Id" });
            DropIndex("dbo.ProfessorEducationalGroups", new[] { "Term_Id" });
            DropIndex("dbo.StudentEducationalClasses", new[] { "Student_Id" });
            DropIndex("dbo.Students", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.Students", new[] { "Term_Id" });
            DropIndex("dbo.Proposals", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.Proposals", new[] { "Student_Id" });
            DropIndex("dbo.Proposals", new[] { "Term_Id" });
            AddColumn("dbo.EducationalGroups", "StudentsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "CancellationStudentsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "DismissedstudentsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "StudentScoresAverage", c => c.Int());
            AddColumn("dbo.EducationalGroups", "ProfessorsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TotalProposals", c => c.Int());
            AddColumn("dbo.EducationalGroups", "ApprovedProposals", c => c.Int());
            AddColumn("dbo.EducationalClasses", "EducationalGroup_Id", c => c.Int());
            AddColumn("dbo.StudentEducationalClasses", "StudentId", c => c.Int());
            CreateIndex("dbo.EducationalClasses", "EducationalGroup_Id");
            AddForeignKey("dbo.EducationalClasses", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            DropColumn("dbo.Professors", "EducationalGroup_Id");
            DropColumn("dbo.StudentEducationalClasses", "Student_Id");
            DropTable("dbo.EducationalClassEducationalGroups");
            DropTable("dbo.ProfessorEducationalGroups");
            DropTable("dbo.Students");
            DropTable("dbo.Proposals");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(),
                        StudentCode = c.String(),
                        EducationalLevel = c.Int(),
                        Name = c.String(),
                        Family = c.String(),
                        Gender = c.Boolean(),
                        NationalCode = c.String(),
                        FatherName = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalGroup_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProfessorEducationalGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalGroup_Id = c.Int(),
                        Professor_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EducationalClassEducationalGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalClass_Id = c.Int(),
                        EducationalGroup_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.StudentEducationalClasses", "Student_Id", c => c.Int());
            AddColumn("dbo.Professors", "EducationalGroup_Id", c => c.Int());
            DropForeignKey("dbo.EducationalClasses", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropIndex("dbo.EducationalClasses", new[] { "EducationalGroup_Id" });
            DropColumn("dbo.StudentEducationalClasses", "StudentId");
            DropColumn("dbo.EducationalClasses", "EducationalGroup_Id");
            DropColumn("dbo.EducationalGroups", "ApprovedProposals");
            DropColumn("dbo.EducationalGroups", "TotalProposals");
            DropColumn("dbo.EducationalGroups", "ProfessorsCount");
            DropColumn("dbo.EducationalGroups", "StudentScoresAverage");
            DropColumn("dbo.EducationalGroups", "DismissedstudentsCount");
            DropColumn("dbo.EducationalGroups", "CancellationStudentsCount");
            DropColumn("dbo.EducationalGroups", "StudentsCount");
            CreateIndex("dbo.Proposals", "Term_Id");
            CreateIndex("dbo.Proposals", "Student_Id");
            CreateIndex("dbo.Proposals", "EducationalGroup_Id");
            CreateIndex("dbo.Students", "Term_Id");
            CreateIndex("dbo.Students", "EducationalGroup_Id");
            CreateIndex("dbo.StudentEducationalClasses", "Student_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "Term_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "Professor_Id");
            CreateIndex("dbo.ProfessorEducationalGroups", "EducationalGroup_Id");
            CreateIndex("dbo.Professors", "EducationalGroup_Id");
            CreateIndex("dbo.EducationalClassEducationalGroups", "Term_Id");
            CreateIndex("dbo.EducationalClassEducationalGroups", "EducationalGroup_Id");
            CreateIndex("dbo.EducationalClassEducationalGroups", "EducationalClass_Id");
            AddForeignKey("dbo.Proposals", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.Proposals", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.Proposals", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            AddForeignKey("dbo.Professors", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            AddForeignKey("dbo.EducationalClassEducationalGroups", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.EducationalClassEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            AddForeignKey("dbo.Students", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.StudentEducationalClasses", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.Students", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "Term_Id", "dbo.Terms", "Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "Professor_Id", "dbo.Professors", "Id");
            AddForeignKey("dbo.ProfessorEducationalGroups", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
            AddForeignKey("dbo.EducationalClassEducationalGroups", "EducationalClass_Id", "dbo.EducationalClasses", "Id");
        }
    }
}

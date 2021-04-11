namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentEducationalClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalClass_Id = c.Int(),
                        Student_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationalClasses", t => t.EducationalClass_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.EducationalClass_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Term_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(),
                        StudentCode = c.String(),
                        Family = c.String(),
                        Gender = c.Boolean(),
                        NationalCode = c.String(),
                        FatherName = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        EducationalGroup_Id = c.Int(),
                        Term_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EducationalGroups", t => t.EducationalGroup_Id)
                .ForeignKey("dbo.Terms", t => t.Term_Id)
                .Index(t => t.EducationalGroup_Id)
                .Index(t => t.Term_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentEducationalClasses", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.Students", "Term_Id", "dbo.Terms");
            DropForeignKey("dbo.StudentEducationalClasses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropForeignKey("dbo.StudentEducationalClasses", "EducationalClass_Id", "dbo.EducationalClasses");
            DropIndex("dbo.Students", new[] { "Term_Id" });
            DropIndex("dbo.Students", new[] { "EducationalGroup_Id" });
            DropIndex("dbo.StudentEducationalClasses", new[] { "Term_Id" });
            DropIndex("dbo.StudentEducationalClasses", new[] { "Student_Id" });
            DropIndex("dbo.StudentEducationalClasses", new[] { "EducationalClass_Id" });
            DropTable("dbo.Students");
            DropTable("dbo.StudentEducationalClasses");
        }
    }
}

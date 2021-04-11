namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalGroups", "AverageBachelorStudentGrades", c => c.Int());
            AddColumn("dbo.EducationalGroups", "AverageMaStudentGrades", c => c.Int());
            AddColumn("dbo.EducationalGroups", "AverageDoctoralStudentGrades", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TeacherToBachelorStudentRatio", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TeacherToMaStudentRatio", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TeacherToDoctoralStudentRatio", c => c.Int());
            AddColumn("dbo.Professors", "AverageGradeEvaluation", c => c.Int());
            DropColumn("dbo.EducationalGroups", "AverageStudentGrades");
            DropColumn("dbo.EducationalGroups", "TeacherToStudentRatio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalGroups", "TeacherToStudentRatio", c => c.Int());
            AddColumn("dbo.EducationalGroups", "AverageStudentGrades", c => c.Int());
            DropColumn("dbo.Professors", "AverageGradeEvaluation");
            DropColumn("dbo.EducationalGroups", "TeacherToDoctoralStudentRatio");
            DropColumn("dbo.EducationalGroups", "TeacherToMaStudentRatio");
            DropColumn("dbo.EducationalGroups", "TeacherToBachelorStudentRatio");
            DropColumn("dbo.EducationalGroups", "AverageDoctoralStudentGrades");
            DropColumn("dbo.EducationalGroups", "AverageMaStudentGrades");
            DropColumn("dbo.EducationalGroups", "AverageBachelorStudentGrades");
        }
    }
}

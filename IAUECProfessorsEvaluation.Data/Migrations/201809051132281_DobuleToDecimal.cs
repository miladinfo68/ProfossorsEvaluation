namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DobuleToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Scores", "MaxValue", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Scores", "MinValue", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.EducationalGroups", "AverageBachelorStudentGrades", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.EducationalGroups", "AverageMaStudentGrades", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.EducationalGroups", "AverageDoctoralStudentGrades", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.EducationalGroups", "TotalStudentScoresAverage", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StudentEducationalClasses", "Grade", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.StudentEducationalClasses", "ProfessorEvaluationScore", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentEducationalClasses", "ProfessorEvaluationScore", c => c.Int());
            AlterColumn("dbo.StudentEducationalClasses", "Grade", c => c.Int());
            AlterColumn("dbo.EducationalGroups", "TotalStudentScoresAverage", c => c.Single());
            AlterColumn("dbo.EducationalGroups", "AverageDoctoralStudentGrades", c => c.Single());
            AlterColumn("dbo.EducationalGroups", "AverageMaStudentGrades", c => c.Single());
            AlterColumn("dbo.EducationalGroups", "AverageBachelorStudentGrades", c => c.Single());
            AlterColumn("dbo.Scores", "MinValue", c => c.Single());
            AlterColumn("dbo.Scores", "MaxValue", c => c.Single());
        }
    }
}

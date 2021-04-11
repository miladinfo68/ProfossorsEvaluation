namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAvrageTypeToFloat : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EducationalGroups", "AverageBachelorStudentGrades", c => c.Single());
            AlterColumn("dbo.EducationalGroups", "AverageMaStudentGrades", c => c.Single());
            AlterColumn("dbo.EducationalGroups", "AverageDoctoralStudentGrades", c => c.Single());
            AlterColumn("dbo.EducationalGroups", "TotalStudentScoresAverage", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EducationalGroups", "TotalStudentScoresAverage", c => c.Int());
            AlterColumn("dbo.EducationalGroups", "AverageDoctoralStudentGrades", c => c.Int());
            AlterColumn("dbo.EducationalGroups", "AverageMaStudentGrades", c => c.Int());
            AlterColumn("dbo.EducationalGroups", "AverageBachelorStudentGrades", c => c.Int());
        }
    }
}

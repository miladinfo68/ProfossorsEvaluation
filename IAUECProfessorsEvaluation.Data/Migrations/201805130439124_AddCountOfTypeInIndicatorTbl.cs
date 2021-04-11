namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountOfTypeInIndicatorTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Indicators", "CountOfType", c => c.String());
            AddColumn("dbo.EducationalGroups", "TotalStudentsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TotalStudentScoresAverage", c => c.Int());
            AddColumn("dbo.EducationalGroups", "TotalProfessorsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "DoctoralProfessorsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "MaProfessorsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "BachelorProfessorsCount", c => c.Int());
            DropColumn("dbo.EducationalGroups", "StudentsCount");
            DropColumn("dbo.EducationalGroups", "StudentScoresAverage");
            DropColumn("dbo.EducationalGroups", "ProfessorsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalGroups", "ProfessorsCount", c => c.Int());
            AddColumn("dbo.EducationalGroups", "StudentScoresAverage", c => c.Int());
            AddColumn("dbo.EducationalGroups", "StudentsCount", c => c.Int());
            DropColumn("dbo.EducationalGroups", "BachelorProfessorsCount");
            DropColumn("dbo.EducationalGroups", "MaProfessorsCount");
            DropColumn("dbo.EducationalGroups", "DoctoralProfessorsCount");
            DropColumn("dbo.EducationalGroups", "TotalProfessorsCount");
            DropColumn("dbo.EducationalGroups", "TotalStudentScoresAverage");
            DropColumn("dbo.EducationalGroups", "TotalStudentsCount");
            DropColumn("dbo.Indicators", "CountOfType");
        }
    }
}

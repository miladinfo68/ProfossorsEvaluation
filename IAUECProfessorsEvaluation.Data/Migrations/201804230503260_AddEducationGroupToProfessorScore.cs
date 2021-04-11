namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEducationGroupToProfessorScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProfessorScores", "EducationalGroup_Id", c => c.Int());
            CreateIndex("dbo.ProfessorScores", "EducationalGroup_Id");
            AddForeignKey("dbo.ProfessorScores", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfessorScores", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropIndex("dbo.ProfessorScores", new[] { "EducationalGroup_Id" });
            DropColumn("dbo.ProfessorScores", "EducationalGroup_Id");
        }
    }
}

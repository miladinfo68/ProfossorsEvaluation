namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Colleges", new[] { "Name", "CollegeCode" });
            DropIndex("dbo.Indicators", new[] { "Name" });
            DropIndex("dbo.ObjectTypes", new[] { "Name" });
            DropIndex("dbo.Ratios", new[] { "Name" });
            DropIndex("dbo.Terms", new[] { "Name" });
            DropIndex("dbo.EducationalGroups", new[] { "EducationalGroupCode", "Name" });
            DropIndex("dbo.Professors", new[] { "ProfessorCode" });
            DropIndex("dbo.EducationalClasses", new[] { "CodeClass", "Name" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.EducationalClasses", new[] { "CodeClass", "Name" });
            CreateIndex("dbo.Professors", "ProfessorCode");
            CreateIndex("dbo.EducationalGroups", new[] { "EducationalGroupCode", "Name" });
            CreateIndex("dbo.Terms", "Name");
            CreateIndex("dbo.Ratios", "Name");
            CreateIndex("dbo.ObjectTypes", "Name");
            CreateIndex("dbo.Indicators", "Name");
            CreateIndex("dbo.Colleges", new[] { "Name", "CollegeCode" });
        }
    }
}

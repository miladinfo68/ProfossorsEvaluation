namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CahngeNameOFEducationalGroupCode : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EducationalGroups", new[] { "CollegeCode", "Name" });
            AddColumn("dbo.EducationalGroups", "EducationalGroupCode", c => c.Int(nullable: false));
            CreateIndex("dbo.EducationalGroups", new[] { "EducationalGroupCode", "Name" });
            DropColumn("dbo.EducationalGroups", "CollegeCode");
            

        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalGroups", "CollegeCode", c => c.Int(nullable: false));
            DropIndex("dbo.EducationalGroups", new[] { "EducationalGroupCode", "Name" });
            DropColumn("dbo.EducationalGroups", "EducationalGroupCode");
            CreateIndex("dbo.EducationalGroups", new[] { "CollegeCode", "Name" });
        }
    }
}

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovePropInEducationClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalClasses", "ProfessorDelayAndEarlier", c => c.Int(nullable: false));
            DropColumn("dbo.EducationalClasses", "ProfessorDelay");
            DropColumn("dbo.EducationalClasses", "ProfessorEarlier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalClasses", "ProfessorEarlier", c => c.Int());
            AddColumn("dbo.EducationalClasses", "ProfessorDelay", c => c.Int());
            DropColumn("dbo.EducationalClasses", "ProfessorDelayAndEarlier");
        }
    }
}

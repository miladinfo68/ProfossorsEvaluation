namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfessorDelayAndEarlierNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EducationalClasses", "ProfessorDelayAndEarlier", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EducationalClasses", "ProfessorDelayAndEarlier", c => c.Int(nullable: false));
        }
    }
}

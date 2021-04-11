namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvrageGradeintoProfessor : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Professors", "AverageGradeEvaluation", c => c.Int());
        }
        
        public override void Down()
        {
            //DropColumn("dbo.Professors", "AverageGradeEvaluation");
        }
    }
}

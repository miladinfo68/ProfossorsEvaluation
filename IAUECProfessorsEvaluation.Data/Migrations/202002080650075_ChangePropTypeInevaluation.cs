namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePropTypeInevaluation : DbMigration
    {
        public override void Up()
        {
            DropColumn("Evaluation.QuestionAnswer", "Term");
        }
        
        public override void Down()
        {
            AddColumn("Evaluation.QuestionAnswer", "Term", c => c.String());
        }
    }
}

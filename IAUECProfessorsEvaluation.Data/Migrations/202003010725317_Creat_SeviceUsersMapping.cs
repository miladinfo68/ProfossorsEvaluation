namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Creat_SeviceUsersMapping : DbMigration
    {
        public override void Up()
        {
            DropColumn("Evaluation.Question", "StartDate");
            DropColumn("Evaluation.Question", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("Evaluation.Question", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("Evaluation.Question", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertyInEvaluation : DbMigration
    {
        public override void Up()
        {
            AddColumn("Evaluation.Question", "ParentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Evaluation.Question", "ParentId");
        }
    }
}

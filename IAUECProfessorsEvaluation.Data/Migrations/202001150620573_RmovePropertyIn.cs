namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RmovePropertyIn : DbMigration
    {
        public override void Up()
        {
            DropColumn("Evaluation.Answer", "CreationDate");
            DropColumn("Evaluation.Answer", "LastModifiedDate");
            DropColumn("Evaluation.Answer", "IsActive");
            DropColumn("Evaluation.Question", "CreationDate");
            DropColumn("Evaluation.Question", "LastModifiedDate");
            DropColumn("Evaluation.Question", "IsActive");
            DropColumn("Evaluation.Comment", "CreationDate");
            DropColumn("Evaluation.Comment", "LastModifiedDate");
            DropColumn("Evaluation.Comment", "IsActive");
            DropColumn("Evaluation.QuestionAnswer", "CreationDate");
            DropColumn("Evaluation.QuestionAnswer", "LastModifiedDate");
            DropColumn("Evaluation.QuestionAnswer", "IsActive");
            DropColumn("Evaluation.SubQuestion", "CreationDate");
            DropColumn("Evaluation.SubQuestion", "LastModifiedDate");
            DropColumn("Evaluation.SubQuestion", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("Evaluation.SubQuestion", "IsActive", c => c.Boolean());
            AddColumn("Evaluation.SubQuestion", "LastModifiedDate", c => c.DateTime());
            AddColumn("Evaluation.SubQuestion", "CreationDate", c => c.DateTime());
            AddColumn("Evaluation.QuestionAnswer", "IsActive", c => c.Boolean());
            AddColumn("Evaluation.QuestionAnswer", "LastModifiedDate", c => c.DateTime());
            AddColumn("Evaluation.QuestionAnswer", "CreationDate", c => c.DateTime());
            AddColumn("Evaluation.Comment", "IsActive", c => c.Boolean());
            AddColumn("Evaluation.Comment", "LastModifiedDate", c => c.DateTime());
            AddColumn("Evaluation.Comment", "CreationDate", c => c.DateTime());
            AddColumn("Evaluation.Question", "IsActive", c => c.Boolean());
            AddColumn("Evaluation.Question", "LastModifiedDate", c => c.DateTime());
            AddColumn("Evaluation.Question", "CreationDate", c => c.DateTime());
            AddColumn("Evaluation.Answer", "IsActive", c => c.Boolean());
            AddColumn("Evaluation.Answer", "LastModifiedDate", c => c.DateTime());
            AddColumn("Evaluation.Answer", "CreationDate", c => c.DateTime());
        }
    }
}

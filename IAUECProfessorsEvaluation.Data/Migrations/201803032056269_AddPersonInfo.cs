namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Professors", "Gender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Professors", "NationalCode", c => c.String());
            AddColumn("dbo.Professors", "FatherName", c => c.String());
            AddColumn("dbo.Professors", "Mobile", c => c.String());
            AddColumn("dbo.Professors", "Email", c => c.String());
            AddColumn("dbo.Professors", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Professors", "Address");
            DropColumn("dbo.Professors", "Email");
            DropColumn("dbo.Professors", "Mobile");
            DropColumn("dbo.Professors", "FatherName");
            DropColumn("dbo.Professors", "NationalCode");
            DropColumn("dbo.Professors", "Gender");
        }
    }
}

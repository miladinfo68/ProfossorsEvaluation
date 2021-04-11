namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addConfigurationForProfessor : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.Professors", "Name", c => c.String(nullable: false, maxLength: 100));
            //AlterColumn("dbo.Professors", "Family", c => c.String(nullable: false, maxLength: 100));
            //AlterColumn("dbo.Professors", "NationalCode", c => c.String(nullable: false, maxLength: 20));
            //AlterColumn("dbo.Professors", "FatherName", c => c.String(nullable: false, maxLength: 100));
            //AlterColumn("dbo.Professors", "Mobile", c => c.String(nullable: false, maxLength: 20));
            //AlterColumn("dbo.Professors", "Email", c => c.String(nullable: false, maxLength: 40));
            //AlterColumn("dbo.Professors", "Address", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.Professors", "Address", c => c.String());
            //AlterColumn("dbo.Professors", "Email", c => c.String());
            //AlterColumn("dbo.Professors", "Mobile", c => c.String());
            //AlterColumn("dbo.Professors", "FatherName", c => c.String());
            //AlterColumn("dbo.Professors", "NationalCode", c => c.String());
            //AlterColumn("dbo.Professors", "Family", c => c.String());
            //AlterColumn("dbo.Professors", "Name", c => c.String(nullable: false, maxLength: 200));
        }
    }
}

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionInLogTblIsMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "Desacription", c => c.String());
        }
        

        public override void Down()
        {
            AlterColumn("dbo.Logs", "Desacription", c => c.String(maxLength: 400));
        }
    }
}

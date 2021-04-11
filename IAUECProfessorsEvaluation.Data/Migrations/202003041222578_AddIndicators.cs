namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndicators : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalGroups", "OnlineHolidays", c => c.Int());
            AddColumn("dbo.EducationalGroups", "OfflineHolidays", c => c.Int());
            AddColumn("dbo.EducationalGroups", "EducationalAndResearchCouncil", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EducationalGroups", "EducationalAndResearchCouncil");
            DropColumn("dbo.EducationalGroups", "OfflineHolidays");
            DropColumn("dbo.EducationalGroups", "OnlineHolidays");
        }
    }
}

namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTblForUniversityLevel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UniversityLevelMappings", "UniversityLevel_Id", "dbo.UniversityLevels");
            DropIndex("dbo.UniversityLevelMappings", new[] { "UniversityLevel_Id" });
            AddColumn("dbo.UniversityLevelMappings", "Score_Id", c => c.Int());
            CreateIndex("dbo.UniversityLevelMappings", "Score_Id");
            AddForeignKey("dbo.UniversityLevelMappings", "Score_Id", "dbo.Scores", "Id");
            DropColumn("dbo.UniversityLevelMappings", "UniversityLevel_Id");
            DropTable("dbo.UniversityLevels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UniversityLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UniversityLevelMappings", "UniversityLevel_Id", c => c.Int());
            DropForeignKey("dbo.UniversityLevelMappings", "Score_Id", "dbo.Scores");
            DropIndex("dbo.UniversityLevelMappings", new[] { "Score_Id" });
            DropColumn("dbo.UniversityLevelMappings", "Score_Id");
            CreateIndex("dbo.UniversityLevelMappings", "UniversityLevel_Id");
            AddForeignKey("dbo.UniversityLevelMappings", "UniversityLevel_Id", "dbo.UniversityLevels", "Id");
        }
    }
}

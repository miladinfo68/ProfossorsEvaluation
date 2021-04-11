namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeId = c.Int(),
                        TypeName = c.String(),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        MappingType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MappingTypes", t => t.MappingType_Id)
                .Index(t => t.MappingType_Id);
            
            CreateTable(
                "dbo.MappingTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UniversityLevelMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UniversityId = c.Int(),
                        UniversityName = c.String(),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                        UniversityLevel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UniversityLevels", t => t.UniversityLevel_Id)
                .Index(t => t.UniversityLevel_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UniversityLevelMappings", "UniversityLevel_Id", "dbo.UniversityLevels");
            DropForeignKey("dbo.Mappings", "MappingType_Id", "dbo.MappingTypes");
            DropIndex("dbo.UniversityLevelMappings", new[] { "UniversityLevel_Id" });
            DropIndex("dbo.Mappings", new[] { "MappingType_Id" });
            DropTable("dbo.UniversityLevels");
            DropTable("dbo.UniversityLevelMappings");
            DropTable("dbo.MappingTypes");
            DropTable("dbo.Mappings");
        }
    }
}

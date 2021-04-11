namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeGroupToGroupCodeInUserTBL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "EducationalGroup_Id", "dbo.EducationalGroups");
            DropIndex("dbo.Users", new[] { "EducationalGroup_Id" });
            AddColumn("dbo.Users", "EducationalGroupCode", c => c.Int(nullable:true));
            DropColumn("dbo.Users", "EducationalGroup_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "EducationalGroup_Id", c => c.Int(nullable: true));
            DropColumn("dbo.Users", "EducationalGroupCode");
            CreateIndex("dbo.Users", "EducationalGroup_Id");
            AddForeignKey("dbo.Users", "EducationalGroup_Id", "dbo.EducationalGroups", "Id");
        }
    }
}

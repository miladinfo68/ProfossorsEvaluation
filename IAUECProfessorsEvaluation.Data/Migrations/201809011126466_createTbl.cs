namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTbl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        Desacription = c.String(maxLength: 400),
                        Date = c.DateTime(nullable: false),
                        LogType_Id = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LogTypes", t => t.LogType_Id)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.LogType_Id)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.LogTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400),
                        CreationDate = c.DateTime(),
                        LastModifiedDate = c.DateTime(),
                        IsActive = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        

        public override void Down()
        {
            DropForeignKey("dbo.Logs", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Logs", "LogType_Id", "dbo.LogTypes");
            DropIndex("dbo.Logs", new[] { "User_ID" });
            DropIndex("dbo.Logs", new[] { "LogType_Id" });
            DropTable("dbo.LogTypes");
            DropTable("dbo.Logs");
        }
    }
}

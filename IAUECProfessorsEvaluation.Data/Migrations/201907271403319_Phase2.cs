namespace IAUECProfessorsEvaluation.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Phase2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terms", "ExamStartDate", c => c.DateTime());
            AddColumn("dbo.Terms", "ExamEndDate", c => c.DateTime());
            AddColumn("dbo.EducationalGroups", "OtherPresenceTime", c => c.Int());
            AddColumn("dbo.EducationalClasses", "ReceiveExamPaperDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "AggregationExamPaperDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "LessonPlanSendDate", c => c.DateTime());
            AddColumn("dbo.EducationalClasses", "ContentType", c => c.Int());
            AddColumn("dbo.EducationalClasses", "HoldingType", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Professors", "DeputyComments", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Professors", "DeputyComments");
            DropColumn("dbo.EducationalClasses", "HoldingType");
            DropColumn("dbo.EducationalClasses", "ContentType");
            DropColumn("dbo.EducationalClasses", "LessonPlanSendDate");
            DropColumn("dbo.EducationalClasses", "AggregationExamPaperDate");
            DropColumn("dbo.EducationalClasses", "ReceiveExamPaperDate");
            DropColumn("dbo.EducationalGroups", "OtherPresenceTime");
            DropColumn("dbo.Terms", "ExamEndDate");
            DropColumn("dbo.Terms", "ExamStartDate");
        }
    }
}

namespace TemplateAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "EventStatus_Id", "dbo.EventStatus");
            DropForeignKey("dbo.Events", "EventType_Id", "dbo.EventTypes");
            DropIndex("dbo.Events", new[] { "EventStatus_Id" });
            DropIndex("dbo.Events", new[] { "EventType_Id" });
            DropColumn("dbo.Events", "Location");
            DropColumn("dbo.Events", "MaxNumberOfAttendees");
            DropColumn("dbo.Events", "MinNumberOfAttendees");
            DropColumn("dbo.Events", "EventCompetitionLevel");
            DropColumn("dbo.Events", "EndTime");
            DropColumn("dbo.Events", "LengthInMinutes");
            DropColumn("dbo.Events", "EventStatus_Id");
            DropColumn("dbo.Events", "EventType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "EventType_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Events", "EventStatus_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Events", "LengthInMinutes", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "EndTime", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.Events", "EventCompetitionLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "MinNumberOfAttendees", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "MaxNumberOfAttendees", c => c.Int());
            AddColumn("dbo.Events", "Location", c => c.String());
            CreateIndex("dbo.Events", "EventType_Id");
            CreateIndex("dbo.Events", "EventStatus_Id");
            AddForeignKey("dbo.Events", "EventType_Id", "dbo.EventTypes", "Id");
            AddForeignKey("dbo.Events", "EventStatus_Id", "dbo.EventStatus", "Id");
        }
    }
}

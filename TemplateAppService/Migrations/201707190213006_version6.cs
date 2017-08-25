namespace TemplateAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventStatus_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Events", "EventType_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Events", "EventStatus_Id");
            CreateIndex("dbo.Events", "EventType_Id");
            AddForeignKey("dbo.Events", "EventStatus_Id", "dbo.EventStatus", "Id");
            AddForeignKey("dbo.Events", "EventType_Id", "dbo.EventTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "EventType_Id", "dbo.EventTypes");
            DropForeignKey("dbo.Events", "EventStatus_Id", "dbo.EventStatus");
            DropIndex("dbo.Events", new[] { "EventType_Id" });
            DropIndex("dbo.Events", new[] { "EventStatus_Id" });
            DropColumn("dbo.Events", "EventType_Id");
            DropColumn("dbo.Events", "EventStatus_Id");
        }
    }
}

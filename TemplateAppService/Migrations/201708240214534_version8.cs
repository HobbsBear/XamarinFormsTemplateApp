namespace TemplateAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version8 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RegisteredEvents");
            AddColumn("dbo.RegisteredEvents", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.RegisteredEvents", "UpdatedAt", c => c.DateTimeOffset(precision: 7));
            AddColumn("dbo.RegisteredEvents", "CreatedAt", c => c.DateTimeOffset(precision: 7));
            AlterColumn("dbo.RegisteredEvents", "UserID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.RegisteredEvents", new[] { "UserID", "EventID" });
            CreateIndex("dbo.RegisteredEvents", "UserID");
            CreateIndex("dbo.RegisteredEvents", "EventID");
            AddForeignKey("dbo.RegisteredEvents", "EventID", "dbo.Events", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RegisteredEvents", "UserID", "dbo.Users", "UserId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegisteredEvents", "UserID", "dbo.Users");
            DropForeignKey("dbo.RegisteredEvents", "EventID", "dbo.Events");
            DropIndex("dbo.RegisteredEvents", new[] { "EventID" });
            DropIndex("dbo.RegisteredEvents", new[] { "UserID" });
            DropPrimaryKey("dbo.RegisteredEvents");
            AlterColumn("dbo.RegisteredEvents", "UserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.RegisteredEvents", "CreatedAt");
            DropColumn("dbo.RegisteredEvents", "UpdatedAt");
            DropColumn("dbo.RegisteredEvents", "isActive");
            AddPrimaryKey("dbo.RegisteredEvents", new[] { "UserId", "EventID" });
        }
    }
}

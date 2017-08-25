namespace TemplateAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Location");
        }
    }
}

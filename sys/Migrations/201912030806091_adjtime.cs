namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjtime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Companies", "StartTime", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "EndTime", c => c.String(nullable: false));
            DropColumn("dbo.Companies", "PrepareTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "PrepareTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Companies", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Companies", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}

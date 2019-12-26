namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjR1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Status", c => c.String(nullable: false));
        }
    }
}

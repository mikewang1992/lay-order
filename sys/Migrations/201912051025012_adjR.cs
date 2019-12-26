namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjR : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Status", c => c.String(nullable: false));
        }
    }
}

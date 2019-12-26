namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjoption : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderDetails", "Options", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderDetails", "Options", c => c.String(nullable: false));
        }
    }
}

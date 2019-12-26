namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMSGandWRONG1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Sent", c => c.Int(nullable: false));
            AlterColumn("dbo.Accounts", "wrong", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "wrong", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Accounts", "Sent", c => c.Boolean(nullable: false));
        }
    }
}

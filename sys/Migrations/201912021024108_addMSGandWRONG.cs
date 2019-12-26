namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMSGandWRONG : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Sent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Accounts", "wrong", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "wrong");
            DropColumn("dbo.Accounts", "Sent");
        }
    }
}

namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjcheckname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "IsCheck", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accounts", "Check");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Check", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accounts", "IsCheck");
        }
    }
}

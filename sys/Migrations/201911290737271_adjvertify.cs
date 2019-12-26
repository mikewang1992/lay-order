namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjvertify : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "Vertify", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Vertify", c => c.Int(nullable: false));
        }
    }
}

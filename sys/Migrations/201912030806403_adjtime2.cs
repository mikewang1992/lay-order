namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjtime2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "PrepareTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "PrepareTime");
        }
    }
}

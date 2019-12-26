namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Tel", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Tel");
        }
    }
}

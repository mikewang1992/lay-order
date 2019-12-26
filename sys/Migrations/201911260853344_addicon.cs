namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addicon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "icon", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permissions", "icon");
        }
    }
}

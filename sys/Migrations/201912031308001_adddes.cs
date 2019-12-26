namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductLists", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductLists", "Description");
        }
    }
}

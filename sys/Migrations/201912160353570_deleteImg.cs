namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteImg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductLists", "Img");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductLists", "Img", c => c.String(nullable: false));
        }
    }
}

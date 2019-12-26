namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addbigQty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "BigQty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "BigQty");
        }
    }
}

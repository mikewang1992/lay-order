namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "Tel", c => c.String(maxLength: 200));
            AddColumn("dbo.Members", "Img", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "Img");
            DropColumn("dbo.Members", "Tel");
        }
    }
}

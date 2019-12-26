namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adjMtime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "initDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "initDate", c => c.String());
        }
    }
}

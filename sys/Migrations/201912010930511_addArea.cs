namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addArea : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        County = c.String(maxLength: 200),
                        Town = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Areas");
        }
    }
}

namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPImg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pid = c.Int(nullable: false),
                        Pimg = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductLists", t => t.Pid, cascadeDelete: true)
                .Index(t => t.Pid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImgs", "Pid", "dbo.ProductLists");
            DropIndex("dbo.ProductImgs", new[] { "Pid" });
            DropTable("dbo.ProductImgs");
        }
    }
}

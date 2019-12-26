namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Pid = c.Int(),
                        Value = c.String(),
                        PermissionName = c.String(),
                        Action = c.String(),
                        Controller = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Permissions", t => t.Pid)
                .Index(t => t.Pid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permissions", "Pid", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "Pid" });
            DropTable("dbo.Permissions");
        }
    }
}

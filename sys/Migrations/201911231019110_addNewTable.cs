namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PermissionCheckboxes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PermissionCheckboxes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(),
                        Action = c.String(),
                        Controller = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
    }
}

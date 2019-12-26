namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermissionCheckboxes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PermissionName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Members", "initDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "initDate");
            DropTable("dbo.PermissionCheckboxes");
        }
    }
}

namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PermissionCheckboxes", "Action", c => c.String());
            AddColumn("dbo.PermissionCheckboxes", "Controller", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PermissionCheckboxes", "Controller");
            DropColumn("dbo.PermissionCheckboxes", "Action");
        }
    }
}

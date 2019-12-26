namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductLists", "ProductCategory_Id", "dbo.ProductCategories");
            DropIndex("dbo.ProductLists", new[] { "ProductCategory_Id" });
            DropColumn("dbo.ProductLists", "PCid");
            RenameColumn(table: "dbo.ProductLists", name: "ProductCategory_Id", newName: "PCid");
            CreateTable(
                "dbo.holidays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Companies", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "EndTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "PrepareTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.ProductLists", "Sides1", c => c.String());
            AddColumn("dbo.ProductLists", "Sides2", c => c.String());
            AddColumn("dbo.ProductLists", "Sides3", c => c.String());
            AddColumn("dbo.ProductLists", "Sides4", c => c.String());
            AlterColumn("dbo.ProductLists", "PCid", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductLists", "PCid", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Cid");
            CreateIndex("dbo.OrderDetails", "Oid");
            CreateIndex("dbo.OrderDetails", "Pid");
            CreateIndex("dbo.ProductLists", "PCid");
            AddForeignKey("dbo.Orders", "Cid", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Oid", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Pid", "dbo.ProductLists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductLists", "PCid", "dbo.ProductCategories", "Id", cascadeDelete: true);
            DropColumn("dbo.Companies", "BusinessHour");
            DropColumn("dbo.ProductLists", "Sides");
            DropTable("dbo.SideDishes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SideDishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sides = c.String(nullable: false),
                        Options = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ProductLists", "Sides", c => c.String());
            AddColumn("dbo.Companies", "BusinessHour", c => c.String(nullable: false));
            DropForeignKey("dbo.ProductLists", "PCid", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderDetails", "Pid", "dbo.ProductLists");
            DropForeignKey("dbo.OrderDetails", "Oid", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Cid", "dbo.Accounts");
            DropIndex("dbo.ProductLists", new[] { "PCid" });
            DropIndex("dbo.OrderDetails", new[] { "Pid" });
            DropIndex("dbo.OrderDetails", new[] { "Oid" });
            DropIndex("dbo.Orders", new[] { "Cid" });
            AlterColumn("dbo.ProductLists", "PCid", c => c.Int());
            AlterColumn("dbo.ProductLists", "PCid", c => c.String(nullable: false));
            DropColumn("dbo.ProductLists", "Sides4");
            DropColumn("dbo.ProductLists", "Sides3");
            DropColumn("dbo.ProductLists", "Sides2");
            DropColumn("dbo.ProductLists", "Sides1");
            DropColumn("dbo.Companies", "PrepareTime");
            DropColumn("dbo.Companies", "EndTime");
            DropColumn("dbo.Companies", "StartTime");
            DropTable("dbo.holidays");
            RenameColumn(table: "dbo.ProductLists", name: "PCid", newName: "ProductCategory_Id");
            AddColumn("dbo.ProductLists", "PCid", c => c.String(nullable: false));
            CreateIndex("dbo.ProductLists", "ProductCategory_Id");
            AddForeignKey("dbo.ProductLists", "ProductCategory_Id", "dbo.ProductCategories", "Id");
        }
    }
}

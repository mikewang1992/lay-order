namespace sys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tel = c.String(nullable: false, maxLength: 10),
                        Password = c.String(),
                        PasswordSalt = c.String(maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 200),
                        Birth = c.String(maxLength: 50),
                        City = c.String(maxLength: 200),
                        Dist = c.String(maxLength: 200),
                        Vertify = c.Int(nullable: false),
                        Check = c.Boolean(nullable: false),
                        IsTable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Img = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessHour = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Oid = c.Int(nullable: false),
                        Pid = c.Int(nullable: false),
                        Options = c.String(nullable: false),
                        Qty = c.Int(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cid = c.Int(nullable: false),
                        OrderTime = c.DateTime(nullable: false),
                        GetTime = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PCName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PCid = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        Img = c.String(nullable: false),
                        Sides = c.String(),
                        ProductCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_Id)
                .Index(t => t.ProductCategory_Id);
            
            CreateTable(
                "dbo.SideDishes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sides = c.String(nullable: false),
                        Options = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductLists", "ProductCategory_Id", "dbo.ProductCategories");
            DropIndex("dbo.ProductLists", new[] { "ProductCategory_Id" });
            DropTable("dbo.Vouchers");
            DropTable("dbo.SideDishes");
            DropTable("dbo.ProductLists");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Companies");
            DropTable("dbo.Banners");
            DropTable("dbo.Accounts");
        }
    }
}

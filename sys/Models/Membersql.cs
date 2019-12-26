namespace sys.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Membersql : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'Membersql' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'sys.Models.Membersql' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'Membersql' 連接字串。
        public Membersql()
            : base("name=Membersql")
        {
        }

       

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Banner> Banners { get; set; }

        public virtual DbSet<Company> CompanySet { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<ProductCategory> ProductCategoryList { get; set; }

        public virtual DbSet<ProductList> ProductLists { get; set; }

        public virtual DbSet<Voucher> Vouchers { get; set; }

        public virtual DbSet<holiday> holiday { get; set; }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<ProductImg> ProductImg { get; set; }

        //騙精靈寫程式要記得砍掉
        //public virtual DbSet<ContactViewModel> ContactViewModel { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
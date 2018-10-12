using AssetMgmt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AssetMgmt.DAL
{
    public class AssetContext : DbContext
    {
        public AssetContext() : base("AssetContext")
        {
            Database.SetInitializer<AssetContext>(null);
        }

        public DbSet<Cash> Cash { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<AssetMgmt.Models.Portfolio> Portfolios { get; set; }

        public System.Data.Entity.DbSet<AssetMgmt.Models.Stock> Stocks { get; set; }

        public System.Data.Entity.DbSet<AssetMgmt.Models.Transaction> Transactions { get; set; }

        public System.Data.Entity.DbSet<AssetMgmt.Models.Divident> Dividents { get; set; }

        public System.Data.Entity.DbSet<AssetMgmt.Models.Profit> Profits { get; set; }

        public System.Data.Entity.DbSet<AssetMgmt.Models.AssetHistory> AssetHistories { get; set; }
    }
}
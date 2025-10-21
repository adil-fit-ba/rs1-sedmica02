using DemoMarket.Logika.Entities.Catalog;
using DemoMarket.Logika.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoMarket.Logika.Data
{
    public partial class DatabaseContext : DbContext
    {
        public DbSet<ProductCategoryEntity> ProductCategories => Set<ProductCategoryEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<MarketUserEntity> Users => Set<MarketUserEntity>();

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}

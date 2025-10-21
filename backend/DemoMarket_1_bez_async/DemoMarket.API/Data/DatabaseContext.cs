using DemoMarket.API.Entities.Catalog;
using DemoMarket.API.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace DemoMarket.API.Data
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

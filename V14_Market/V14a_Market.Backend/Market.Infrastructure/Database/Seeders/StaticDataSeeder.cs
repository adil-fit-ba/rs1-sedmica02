namespace Market.Infrastructure.Database.Seeders;

public partial class StaticDataSeeder
{
    private static DateTime DateTime { get; set; } = new DateTime(2022, 4, 13, 1, 22, 18, 866, DateTimeKind.Local);

    public static void Seed(ModelBuilder modelBuilder)
    {
        // Static data is added in the migration
        // if it does not exist in the DB at the time of creating the migration
        // example of static data: roles
        SeedProductCategories(modelBuilder);
        SeedPromotions(modelBuilder);
    }

    private static void SeedProductCategories(ModelBuilder modelBuilder)
    {
        // todo: user roles

        //modelBuilder.Entity<UserRoles>().HasData(new List<UserRoleEntity>
        //{
        //    new UserRoleEntity{
        //        Id = 1,
        //        Name = "Admin",
        //        CreatedAt = dateTime,
        //        ModifiedAt = null,
        //    },
        //    new UserRoleEntity{
        //        Id = 2,
        //        Name = "Employee",
        //        CreatedAt = dateTime,
        //        ModifiedAt = null,
        //    },
        //});
    }

    private static void SeedPromotions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PromotionEntity>().HasData(new List<PromotionEntity>
        {
            new PromotionEntity
            {
                Id = 1,
                Title = "Zimska rasprodaja - Do 50% popusta!",
                ImageUrl = "/images/promotions/winter-sale.jpg",
                TargetUrl = "/products?category=zimska-oprema",
                IsActive = true,
                StartsAtUtc = null,
                EndsAtUtc = null,
                SortOrder = 1,
                CreatedAtUtc = DateTime,
                IsDeleted = false
            },
            new PromotionEntity
            {
                Id = 2,
                Title = "Novi proizvodi - Pogledajte kolekciju",
                ImageUrl = "/images/promotions/new-arrivals.jpg",
                TargetUrl = "/products?sort=newest",
                IsActive = true,
                StartsAtUtc = null,
                EndsAtUtc = null,
                SortOrder = 2,
                CreatedAtUtc = DateTime,
                IsDeleted = false
            },
            new PromotionEntity
            {
                Id = 3,
                Title = "Besplatna dostava za narudžbe preko 50 BAM",
                ImageUrl = "/images/promotions/free-shipping.jpg",
                TargetUrl = null,
                IsActive = true,
                StartsAtUtc = null,
                EndsAtUtc = null,
                SortOrder = 3,
                CreatedAtUtc = DateTime,
                IsDeleted = false
            },
            new PromotionEntity
            {
                Id = 4,
                Title = "Akcija - Brendirana oprema -30%",
                ImageUrl = "/images/promotions/brand-sale.jpg",
                TargetUrl = "/products?brand=premium",
                IsActive = true,
                StartsAtUtc = null,
                EndsAtUtc = null,
                SortOrder = 4,
                CreatedAtUtc = DateTime,
                IsDeleted = false
            },
            new PromotionEntity
            {
                Id = 5,
                Title = "Loyalty program - Sakupljajte bodove",
                ImageUrl = "/images/promotions/loyalty.jpg",
                TargetUrl = "/loyalty",
                IsActive = true,
                StartsAtUtc = null,
                EndsAtUtc = null,
                SortOrder = 5,
                CreatedAtUtc = DateTime,
                IsDeleted = false
            }
        });
    }
}

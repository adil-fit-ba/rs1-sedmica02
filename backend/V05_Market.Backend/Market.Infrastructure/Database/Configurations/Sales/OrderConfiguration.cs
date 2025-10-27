using Market.Domain.Entities.Sales;

namespace Market.Infrastructure.Database.Configurations.Catelog;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder
            .ToTable("Products");

    }
}

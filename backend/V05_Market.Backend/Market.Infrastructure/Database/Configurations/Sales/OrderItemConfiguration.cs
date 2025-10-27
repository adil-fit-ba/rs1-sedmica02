using Market.Infrastructure.Database.Configurations.Sales;

namespace Market.Infrastructure.Database.Configurations.Catelog;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder
            .ToTable("OrderItems");


    }
}
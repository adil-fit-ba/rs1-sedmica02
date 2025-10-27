using Market.Domain.Common;
using Market.Domain.Entities.Catalog;
using Market.Domain.Entities.Sales;

namespace Market.Infrastructure.Database.Configurations.Sales
{
    public class OrderItemEntity: BaseEntity
    {
        public required int OrderId { get; set; }
        public OrderEntity? Order { get; set; }

        public required int ProductId { get; set; }
        public ProductEntity? Product { get; set; }

        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public decimal? DiscountAmount { get; set; }
    }
}

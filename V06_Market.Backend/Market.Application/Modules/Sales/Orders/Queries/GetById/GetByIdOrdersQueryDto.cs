using Market.Domain.Entities.Sales;

namespace Market.Application.Modules.Sales.Orders.Queries.GetById;

public sealed class GetByIdOrdersQueryDto
{
    public required int Id { get; init; }
    public required string ReferenceNumber { get; init; }
    public required GetByIdOrdersQueryDtoUser User { get; init; }
    public required DateTime OrderedAtUtc { get; set; }
    public required DateTime? PaidAtUtc { get; set; }
    public required OrderStatusType Status { get; set; }
    public required decimal TotalAmount { get; set; }
    public required string? Note { get; set; }
    public required List<GetByIdOrdersQueryDtoItem> Items { get; set; }

}
public sealed class GetByIdOrdersQueryDtoUser
{
    public required string UserFirstname { get; set; }
    public required string UserLastname { get; set; }
    public required string UserAddress { get; set; }//todo
    public required string UserCity { get; set; }//todo
}

public class GetByIdOrdersQueryDtoItem
{
    public required int Id { get; set; }
    public required GetByIdOrdersQueryDtoItemProduct Product { get; set; }
    public required decimal Quantity { get; set; }
    public required decimal UnitPrice { get; set; }
    public required decimal Subtotal { get; set; }
    public required decimal? DiscountAmount { get; set; }
    public required decimal? DiscountPercent { get; set; }
    public required decimal Total { get; set; }

}

public class GetByIdOrdersQueryDtoItemProduct
{
    public required int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductCategoryName { get; set; }

}
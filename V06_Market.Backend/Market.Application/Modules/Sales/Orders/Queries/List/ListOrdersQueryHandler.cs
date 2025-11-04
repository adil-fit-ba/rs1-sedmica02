namespace Market.Application.Modules.Catalog.Products.Queries.List;

public sealed class ListOrdersQueryHandler(IAppDbContext ctx)
        : IRequestHandler<ListOrdersQuery, PageResult<ListOrdersQueryDto>>
{

    public async Task<PageResult<ListOrdersQueryDto>> Handle(ListOrdersQuery request, CancellationToken ct)
    {
        var q = ctx.Orders.AsNoTracking();

        var searchTerm = request.Search?.Trim().ToLower() ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            q = q.Where(x => x.ReferenceNumber.ToLower().Contains(searchTerm));
        }

        var projectedQuery = q.OrderBy(x => x.OrderedAtUtc)
            .Select(x => new ListOrdersQueryDto
            {
                Id = x.Id,
                ReferenceNumber = x.ReferenceNumber,
                User = new ListOrdersQueryDtoUser
                {
                    UserFirstname = x.MarketUser!.Firstname,
                    UserLastname = x.MarketUser!.Lastname,
                    UserAddress = "Todo",//todo: ticket no 126
                    UserCity = "Todo",//todo: ticket no 126
                },
                OrderedAtUtc = x.OrderedAtUtc,
                PaidAtUtc = x.PaidAtUtc,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                Note = x.Note,
                //"x.Items" ili "ctx.OrderItems.Where(x => x.OrderId == x.Id)"
                Items = x.Items.Select(i => new ListOrdersQueryDtoItem
                {
                    Product = new ListOrdersQueryDtoItemProduct
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product!.Name,
                        ProductCategoryName = i.Product!.Category!.Name
                    },
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Subtotal = i.Subtotal,
                    DiscountAmount = i.DiscountAmount,
                    DiscountPercent = i.DiscountPercent,
                    Total = i.Total
                }).ToList()
            });

        return await PageResult<ListOrdersQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}

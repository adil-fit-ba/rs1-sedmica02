namespace Market.Application.Modules.Sales.Orders.Queries.GetById;

public sealed class GetByIdOrdersQueryHandler(IAppDbContext context, IAppCurrentUser currentUser)
        : IRequestHandler<GetByIdOrdersQuery, GetByIdOrdersQueryDto>
{
    public async Task<GetByIdOrdersQueryDto> Handle(GetByIdOrdersQuery request, CancellationToken ct)
    {
        var q =  context.Orders
            .Where(c => c.Id == request.Id)
            .AsNoTracking();

        if (!currentUser.IsAdmin)
        {
            q = q.Where(o => o.MarketUserId == currentUser.UserId);
        }

        var order = await q
            .Select(x => new GetByIdOrdersQueryDto
            {
                Id = x.Id,
                ReferenceNumber = x.ReferenceNumber,
                User = new ()
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
                Items = x.Items.Select(i => new GetByIdOrdersQueryDtoItem
                {
                    Id = i.Id,
                    Product = new ()
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
            })
            .FirstOrDefaultAsync(ct);

        if (order == null)
        {
            throw new MarketNotFoundException($"Order with Id {request.Id} not found.");
        }

        return order;
    }
}

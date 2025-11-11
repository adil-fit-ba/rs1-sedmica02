using Market.Domain.Entities.Sales;

namespace Market.Application.Modules.Sales.Orders.Commands.Create;

public class CreateOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser currentUser)
    : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken ct)
    {

        var order = new OrderEntity
        {
            ReferenceNumber = Guid.NewGuid().ToString().Substring(0, 5).ToUpper(),
            MarketUserId = currentUser.UserId!.Value,
            OrderedAtUtc = DateTime.UtcNow,
            Status = OrderStatusType.Draft,
            TotalAmount = 0m, //
            Note = request.Note
        };
        ctx.Orders.Add(order);
        await ctx.SaveChangesAsync(ct);

        foreach (var item in request.Items)
        {
            var product = await ctx.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == item.ProductId, ct);

            //TODO: koristiti rollback transaction ako bilo koji item nije validan
            if (product is null)
            {
                throw new ValidationException(message: $"Invalid productId {item.ProductId}.");
            }

            if (product.IsEnabled == false)
            {
                throw new ValidationException($"Product {product.Name} is disabled.");
            }

            decimal subtotal = product.Price * item.Quantity;

            decimal discountPercent = 0.05m;
            decimal discountAmount = subtotal * discountPercent;
            decimal total = subtotal - discountAmount;

            var orderItem = new OrderItemEntity
            {
                OrderId = order.Id,//ako koristimo id onda mora SaveChanges prije od Order biti prije toga
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                Subtotal = subtotal,
                DiscountPercent = discountPercent,
                DiscountAmount = discountAmount,
                Total = total
            };

            ctx.OrderItems.Add(orderItem);
            order.TotalAmount += orderItem.Total;
        }

        await ctx.SaveChangesAsync(ct);

        return order.Id;
    }
}
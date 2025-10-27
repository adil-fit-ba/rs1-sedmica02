﻿using Market.Domain.Entities.Sales;

namespace Market.Application.Modules.Catalog.Products.Commands.Create;

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

            var orderItem = new OrderItemEntity
            {
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product!.Price,
                Subtotal = product!.Price * item.Quantity,
                DiscountAmount = 0.05m,
                Total = (1 - 0.05m) * (product!.Price * item.Quantity)
            };

            ctx.OrderItems.Add(orderItem);
            order.TotalAmount += orderItem.Total;
        }

        await ctx.SaveChangesAsync(ct);

        return order.Id;
    }
}
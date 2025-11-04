using Market.Domain.Entities.Sales;

namespace Market.Application.Modules.Sales.Orders.Commands.Update;

public class UpdateOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser currentUser)
    : IRequestHandler<UpdateOrderCommand, int>
{
    public async Task<int> Handle(UpdateOrderCommand request, CancellationToken ct)
    {

        var order = await ctx.Orders
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(ct);

        if (order is null)
            throw new MarketNotFoundException($"Orders (ID={request.Id}) nije pronađen.");

        if (order.Status != OrderStatusType.Draft)
            throw new ValidationException("Only draft orders can be updated.");

        order.Note = request.Note;
        //itd.
        await ctx.SaveChangesAsync(ct);

        order.TotalAmount = 0;

        //todo: brisanje starih itema

        foreach (var item in request.Items)
        {
            var product = await ctx.Products
               .AsNoTracking()
               .FirstOrDefaultAsync(p => p.Id == item.ProductId, ct);

            if (product is null)
            {
                throw new ValidationException($"Invalid productId {item.ProductId}.");
            }

            if (product.IsEnabled == false)
            {
                throw new ValidationException($"Product {product.Name} is disabled.");
            }

            decimal subtotal = product.Price * item.Quantity;

            decimal discountPercent = 0.05m;
            decimal discountAmount = subtotal * discountPercent;
            decimal total = subtotal - discountAmount;


            //jel insert
            if (item.Id == 0)
            {
                // insert logic
                var orderItem = new OrderItemEntity
                {
                    OrderId = order.Id,
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
            else
            {
                //update logic

                var orderItem = ctx.OrderItems
                    .Where(oi => oi.Id == item.Id && oi.OrderId == order.Id)
                    .FirstOrDefault();

                if (orderItem is null)
                {
                    throw new ValidationException($"Order item (ID={item.Id}) not found in order (ID={order.Id}).");
                }

                orderItem.OrderId = order.Id;
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Quantity;
                orderItem.UnitPrice = product.Price;
                orderItem.Subtotal = subtotal;
                orderItem.DiscountPercent = discountPercent;
                orderItem.DiscountAmount = discountAmount;
                orderItem.Total = total;

                order.TotalAmount += orderItem.Total;
            }
        }

        await ctx.SaveChangesAsync(ct);

        return order.Id;
    }
}
using Market.Domain.Entities.Sales;

namespace Market.Application.Modules.Sales.Orders.Commands.Update;

public class UpdateOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser currentUser)
    : IRequestHandler<UpdateOrderCommand, int>
{
    public async Task<int> Handle(UpdateOrderCommand request, CancellationToken ct)
    {
        #region Fetch Order, set properties and order items
        OrderEntity? order = await ctx.Orders
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(ct);

        if (order is null)
            throw new MarketNotFoundException($"Orders (ID={request.Id}) nije pronađen.");

        if (order.Status != OrderStatusType.Draft)
            throw new ValidationException("Only draft orders can be updated.");

        order.Note = request.Note;
        order.TotalAmount = 0; //reset prije ponovnog izračuna (sumiranja)

        // pokupiti sve postojeće stavke narudžbe iz baze podataka - 1 (jedan) upit
        Dictionary<int, OrderItemEntity> existingOrderItems = await ctx.OrderItems
            .Where(oi => oi.OrderId == order.Id)
            .ToDictionaryAsync(oi => oi.Id, ct);
        #endregion

        #region Delete order items that are not present in the API request

        // Build a set of item IDs that should remain (only existing items have Id)
        var keepIds = request.Items
            .Where(i => i.Id.HasValue)
            .Select(i => i.Id!.Value)
            .ToHashSet();

        // Delete all order items not present in the request
        var itemsToDelete = existingOrderItems
            .Where(oi => !keepIds.Contains(oi.Key))// posto je keepIds tipa hashSet, ovo je O(1) umjesto O(n)
            .Select(oi => oi.Value)
            .ToList();

        // izbrisati te stavke iz konteksta
        ctx.OrderItems.RemoveRange(itemsToDelete);

        #endregion

        #region Fetch Products form DB and validate

        // pokupiti sve id-ove proizvoda koji se naručuju
        List<int> productIds = request.Items
            .Select(ri => ri.ProductId)
            .Distinct()
            .ToList();

        // pokupiti sve entity objekte proizvoda koji se naručuju
        // ovo je samo jedan (1) upit da pokupi sve proizvode !
        List<ProductEntity> products = await ctx.Products
            .Where(p => productIds.Contains(p.Id))
            .AsNoTracking()
            .ToListAsync(ct);

        var productsDict = products.ToDictionary(p => p.Id);

        var missing = productIds.Except(productsDict.Keys).ToArray();
        if (missing.Length > 0)
            throw new ValidationException($"Unknown product ids: {string.Join(", ", missing)}");

        var disabled = products.Where(p => !p.IsEnabled).Select(p => p.Name).ToArray();
        if (disabled.Length > 0)
            throw new ValidationException($"Disabled products: {string.Join(", ", disabled)}");

        #endregion

        #region Create or update order items
        // For demo: flat discount 5%. In real code: inject IPriceCalculator
        decimal discountPercent = 0.05m;

        foreach (UpdateOrderCommandItem requestItem in request.Items)
        {
            ProductEntity product = productsDict[requestItem.ProductId];// O(1) pristup umjesto O(n)

            decimal subtotal = RoundMoney(product.Price * requestItem.Quantity);
            decimal discountAmount = RoundMoney(subtotal * discountPercent);
            decimal total = RoundMoney(subtotal - discountAmount);

            OrderItemEntity? orderItem = null;

            //jel insert
            if (requestItem.Id == null)
            {
                // insert logic
                orderItem = new OrderItemEntity
                {
                    Order = order,
                    ProductId = requestItem.ProductId,
                    Quantity = requestItem.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = subtotal,
                    DiscountPercent = discountPercent,
                    DiscountAmount = discountAmount,
                    Total = total
                };

                ctx.OrderItems.Add(orderItem);
            }
            else
            {
                //update logic

                orderItem = existingOrderItems!.GetValueOrDefault(requestItem.Id.Value);

                if (orderItem is null)
                {
                    throw new ValidationException($"Order item (ID={requestItem.Id}) not found in order (ID={order.Id}).");
                }

                orderItem.ProductId = requestItem.ProductId;
                orderItem.Quantity = requestItem.Quantity;
                orderItem.UnitPrice = product.Price;
                orderItem.Subtotal = subtotal;
                orderItem.DiscountPercent = discountPercent;
                orderItem.DiscountAmount = discountAmount;
                orderItem.Total = total;
            }

            // Each line is already rounded, but we round again defensively to ensure header totals stay consistent
            // even if future logic changes (e.g. new taxes, discounts, or rounding mode adjustments)
            order.TotalAmount += RoundMoney(orderItem.Total);
        }
        #endregion

        await ctx.SaveChangesAsync(ct);

        return order.Id;
    }
    private static decimal RoundMoney(decimal value)
        => Math.Round(value, 2, MidpointRounding.AwayFromZero);
}
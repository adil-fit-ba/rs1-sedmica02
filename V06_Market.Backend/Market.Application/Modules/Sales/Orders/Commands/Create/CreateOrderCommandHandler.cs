using Market.Domain.Entities.Sales;

namespace Market.Application.Modules.Sales.Orders.Commands.Create;

public class CreateOrderCommandHandler(IAppDbContext ctx, IAppCurrentUser currentUser)
    : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken ct)
    {
        #region Create Order and set properties
        var order = new OrderEntity
        {
            ReferenceNumber = GenerateReference(),
            MarketUserId = currentUser.UserId!.Value,
            OrderedAtUtc = DateTime.UtcNow,
            Status = OrderStatusType.Draft,
            TotalAmount = 0m, //
            Note = request.Note
        };
        ctx.Orders.Add(order);

        #endregion

        #region Fetch Products form DB and validate
        // pokupiti sve id-ove proizvoda koji se naručuju
        List<int> productIds = request.Items.Select(ri => ri.ProductId).ToList();

        // pokupiti sve entity objekte proizvoda koji se naručuju
        // ovo je jedan (1) upit za sve proizvode ! - važno za performanse
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

        #region Create order items
        // For demo: flat discount 5%. In real code: inject IPriceCalculator
        decimal discountPercent = 0.05m;

        foreach (CreateOrderCommandItem requestItem in request.Items)
        {
            ProductEntity product = productsDict[requestItem.ProductId];// O(1) pristup umjesto O(n)

            decimal subtotal = RoundMoney(product.Price * requestItem.Quantity);
            decimal discountAmount = RoundMoney(subtotal * discountPercent);
            decimal total = RoundMoney(subtotal - discountAmount);

            var orderItem = new OrderItemEntity
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

    private static string GenerateReference()
        => Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
}
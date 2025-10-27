﻿namespace Market.Application.Modules.Catalog.Products.Commands.Create;

public class CreateProductCommandHandler(IAppDbContext ctx)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var normalized = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(normalized))
            throw new ValidationException("Name is required.");

        // Check if a product with the same name already exists.
        bool exists = await ctx.Products
            .AnyAsync(x => x.Name == normalized, ct);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        var productCategory = await ctx.ProductCategories
            .Where(x => x.Id == request.CategoryId)
            .FirstOrDefaultAsync(ct);

        if (productCategory is null)
        {
            throw new ValidationException("Invalid CategoryId.");
        }

        if (productCategory.IsEnabled == false)
        {
            throw new ValidationException($"Category {productCategory.Name} is disabled.");
        }

        var category = new ProductEntity
        {
            Name = request.Name!.Trim(),
            Description = request.Description?.Trim(),
            Price = request.Price,
            StockQuantity = 0, // default StockQuantity
            CategoryId = request.CategoryId,
            IsEnabled = true // deault IsEnabled
        };

        ctx.Products.Add(category);
        await ctx.SaveChangesAsync(ct);

        return category.Id;
    }
}
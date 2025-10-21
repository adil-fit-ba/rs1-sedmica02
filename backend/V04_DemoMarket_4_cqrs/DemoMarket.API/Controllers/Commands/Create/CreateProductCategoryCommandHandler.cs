using Azure.Core;
using DemoMarket.API.Common.Exceptions;
using DemoMarket.Logika.Data;
using DemoMarket.Logika.Entities.Catalog;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DemoMarket.API.Controllers.Commands.Create;

public class CreateProductCategoryCommandHandler(DatabaseContext db) : IRequestHandler<CreateProductCategoryCommand, int>
{
    public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken ct)
    {
        var normalized = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(normalized))
            throw new ValidationException("Name is required.");

        // Check if a category with the same name already exists.
        bool exists = await db.ProductCategories.AnyAsync(x => x.Name == normalized, ct);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        var category = new ProductCategoryEntity
        {
            Name = normalized,
            IsEnabled = true // default IsEnabled
        };

        await db.ProductCategories.AddAsync(category, ct);
        await db.SaveChangesAsync(ct);

        return category.Id;
    }

}
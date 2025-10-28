using DemoMarket.API.Common;
using DemoMarket.API.Common.Exceptions;
using DemoMarket.API.Controllers.Commands.Create;
using DemoMarket.API.Controllers.Commands.Update;
using DemoMarket.API.Controllers.Queries.GetById;
using DemoMarket.API.Controllers.Queries.List;
using DemoMarket.Logika.Data;
using DemoMarket.Logika.Entities.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DemoMarket.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProductCategoriesController(DatabaseContext db) : ControllerBase
{
    // POST /productcategories
    [HttpPost]
    public async Task<ActionResult<int>> CreateProductCategory([FromBody] CreateProductCategoryCommand request, CancellationToken ct)
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

        return Ok(new { id = category.Id });
    }

    // PUT /productcategories/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateProductCategoryCommand request, CancellationToken ct)
    {
        var entity = db.ProductCategories
           .Where(x => x.Id == id)
           .FirstOrDefault();

        if (entity is null)
            throw new MarketNotFoundException($"Kategorija (ID={id}) nije pronađena.");

        // Check for duplicate name (case-insensitive, except for the same ID)
        var exists = await db.ProductCategories
            .AnyAsync(x => x.Id != id && x.Name.ToLower() == request.Name.ToLower(), ct);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        entity.Name = request.Name.Trim();

        await db.SaveChangesAsync(ct);
        return NoContent();
    }

    // DELETE /productcategories/{id}
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken ct)
    {
        var category = await db.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (category is null)
            throw new MarketNotFoundException("Kategorija nije pronađena.");

        db.ProductCategories.Remove(category);
        await db.SaveChangesAsync(ct);

        return NoContent();
    }

    // GET /productcategories/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<GetProductCategoryByIdQueryDto>> GetById(int id, CancellationToken ct)
    {
        var category = await db.ProductCategories
            .Where(c => c.Id == id)
            .Select(x => new GetProductCategoryByIdQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled
            })
            .FirstOrDefaultAsync(ct);

        if (category == null)
        {
            throw new MarketNotFoundException($"Product category with Id {id} not found.");
        }

        return category;
    }

    // GET /productcategories?Search=...&Skip=0&Take=20
    [HttpGet()]
    public async Task<PageResult<ListProductCategoriesQueryDto>> List([FromQuery] ListProductCategoriesQuery request, CancellationToken ct)
    {
        IQueryable<ProductCategoryEntity> q = db.ProductCategories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            q = q.Where(x => x.Name.Contains(request.Search));
        }

        if (request.OnlyEnabled is not null)
            q = q.Where(x => x.IsEnabled == request.OnlyEnabled);

        var projectedQuery = q.OrderBy(x => x.Name)
            .Select(x => new ListProductCategoriesQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled
            });

        return await PageResult<ListProductCategoriesQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
    }

    // PUT /productcategories/{id}/disable
    [HttpPut("{id:int}/disable")]
    public async Task<ActionResult> Disable(int id, CancellationToken ct)
    {
        var cat = await db.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (cat is null)
        {
            throw new MarketNotFoundException($"Kategorija (ID={id}) nije pronađena.");
        }

        if (!cat.IsEnabled) return Ok(); // idempotent

        // Business rule: cannot disable if there are active products
        var hasActiveProducts = db.Products
            .Any(p => p.CategoryId == cat.Id && p.IsEnabled);

        if (hasActiveProducts)
        {
            throw new MarketBusinessRuleException(
                "category.disable.blocked.activeProducts",
                $"Category {cat.Name} cannot be disabled because it contains active products."
            );
        }

        cat.IsEnabled = false;
        await db.SaveChangesAsync(ct);

        return Ok();
    }

    // PUT /productcategories/{id}/enable
    [HttpPut("{id:int}/enable")]
    public async Task<ActionResult> Enable(int id, CancellationToken ct)
    {
        var entity = await db.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (entity is null)
            throw new MarketNotFoundException($"Kategorija (ID={id}) nije pronađena.");

        if (!entity.IsEnabled)
        {
            entity.IsEnabled = true;
            await db.SaveChangesAsync(ct);
        }

        return NoContent();
    }
}

using DemoMarket.API.Common;
using DemoMarket.API.Common.Exceptions;
using DemoMarket.API.Controllers.Commands.Create;
using DemoMarket.API.Controllers.Commands.Update;
using DemoMarket.API.Controllers.Queries.GetById;
using DemoMarket.API.Controllers.Queries.List;
using DemoMarket.API.Data;
using DemoMarket.API.Entities.Catalog;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DemoMarket.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class ProductCategoriesController(DatabaseContext db) : ControllerBase
{
    // POST /productcategories
    [HttpPost]
    public ActionResult<int> CreateProductCategory([FromBody] CreateProductCategoryCommand request)
    {
        var normalized = request.Name?.Trim();

        if (string.IsNullOrWhiteSpace(normalized))
            throw new ValidationException("Name is required.");

        // Check if a category with the same name already exists.
        bool exists = db.ProductCategories.Any(x => x.Name == normalized);

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        var category = new ProductCategoryEntity
        {
            Name = normalized,
            IsEnabled = true // default IsEnabled
        };

        db.ProductCategories.Add(category);
        db.SaveChanges();

        return Ok(new { id = category.Id });
    }

    // PUT /productcategories/{id}
    [HttpPut("{id}")]
    public ActionResult Update(int id, [FromBody] UpdateProductCategoryCommand request)
    {
        var entity = db.ProductCategories
           .Where(x => x.Id == id)
           .FirstOrDefault();

        if (entity is null)
            throw new MarketNotFoundException($"Kategorija (ID={id}) nije pronađena.");

        // Check for duplicate name (case-insensitive, except for the same ID)
        var exists = db.ProductCategories
            .Any(x => x.Id != id && x.Name.ToLower() == request.Name.ToLower());

        if (exists)
        {
            throw new MarketConflictException("Name already exists.");
        }

        entity.Name = request.Name.Trim();

        db.SaveChanges();
        return NoContent();
    }

    // DELETE /productcategories/{id}
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var category = db.ProductCategories
            .FirstOrDefault(x => x.Id == id);

        if (category is null)
            throw new MarketNotFoundException("Kategorija nije pronađena.");

        db.ProductCategories.Remove(category);
        db.SaveChanges();

        return NoContent();
    }

    // GET /productcategories/{id}
    [HttpGet("{id}")]
    public ActionResult<GetProductCategoryByIdQueryDto> GetById(int id)
    {
        var category = db.ProductCategories
            .Where(c => c.Id == id)
            .Select(x => new GetProductCategoryByIdQueryDto
            {
                Id = x.Id,
                Name = x.Name,
                IsEnabled = x.IsEnabled
            })
            .FirstOrDefault();

        if (category == null)
        {
            throw new MarketNotFoundException($"Product category with Id {id} not found.");
        }

        return category;
    }

    // GET /productcategories?Search=...&Skip=0&Take=20
    [HttpGet()]
    public PageResult<ListProductCategoriesQueryDto> List([FromQuery] ListProductCategoriesQuery request)
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

        return PageResult<ListProductCategoriesQueryDto>.FromQueryable(projectedQuery, request.Paging);
    }

    // PUT /productcategories/{id}/disable
    [HttpPut("{id:int}/disable")]
    public ActionResult Disable(int id)
    {
        var cat = db.ProductCategories
            .FirstOrDefault(x => x.Id == id);

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
        db.SaveChanges();

        return Ok();
    }

    // PUT /productcategories/{id}/enable
    [HttpPut("{id:int}/enable")]
    public ActionResult Enable(int id)
    {
        var entity = db.ProductCategories
            .FirstOrDefault(x => x.Id == id);

        if (entity is null)
            throw new MarketNotFoundException($"Kategorija (ID={id}) nije pronađena.");

        if (!entity.IsEnabled)
        {
            entity.IsEnabled = true;
            db.SaveChanges();
        }

        return NoContent();
    }
}

namespace Market.Application.Modules.Catalog.ProductCategories.Queries.List;

public sealed class ListProductsQuery : BasePagedQuery<ListProductsQueryDto>
{
    public string? Search { get; init; }
}

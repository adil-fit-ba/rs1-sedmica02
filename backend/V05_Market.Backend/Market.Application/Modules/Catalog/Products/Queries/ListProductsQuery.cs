namespace Market.Application.Modules.Catalog.Products.Queries;

public sealed class ListProductsQuery : BasePagedQuery<ListProductsQueryDto>
{
    public string? Search { get; init; }
}

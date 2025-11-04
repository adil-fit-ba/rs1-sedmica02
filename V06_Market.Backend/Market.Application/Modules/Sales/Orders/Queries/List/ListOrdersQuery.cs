namespace Market.Application.Modules.Catalog.Products.Queries.List;

public sealed class ListOrdersQuery : BasePagedQuery<ListOrdersQueryDto>
{
    public string? Search { get; init; }
}

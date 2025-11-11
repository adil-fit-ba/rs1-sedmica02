namespace Market.Application.Modules.Catalog.Products.Queries.List;

public sealed class ListOrdersWithItemsQuery : BasePagedQuery<ListOrdersWithItemsQueryDto>
{
    public string? Search { get; init; }
}

using DemoMarket.API.Common;

namespace DemoMarket.API.Controllers.Queries.List;

public sealed class ListProductCategoriesQuery : BasePagedQuery<ListProductCategoriesQueryDto>
{
    public string? Search { get; init; }
    public bool? OnlyEnabled { get; init; }
}

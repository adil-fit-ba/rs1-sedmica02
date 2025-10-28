namespace DemoMarket.API.Controllers.Queries.List;

public sealed class ListProductCategoriesQueryDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required bool IsEnabled { get; init; }
}

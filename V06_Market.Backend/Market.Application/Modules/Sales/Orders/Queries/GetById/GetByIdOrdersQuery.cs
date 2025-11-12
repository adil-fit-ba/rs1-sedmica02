namespace Market.Application.Modules.Sales.Orders.Queries.GetById;

public sealed class GetByIdOrdersQuery : IRequest<GetByIdOrdersQueryDto>
{
    public int Id { get; set; }
}

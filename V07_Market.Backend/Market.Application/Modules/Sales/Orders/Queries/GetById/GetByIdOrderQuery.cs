namespace Market.Application.Modules.Sales.Orders.Queries.GetById;

public sealed class GetByIdOrderQuery : IRequest<GetByIdOrderQueryDto>
{
    public int Id { get; set; }
}

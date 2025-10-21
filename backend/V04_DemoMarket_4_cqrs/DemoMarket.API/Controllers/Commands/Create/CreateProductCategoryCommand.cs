using MediatR;

namespace DemoMarket.API.Controllers.Commands.Create;

public class CreateProductCategoryCommand : IRequest<int>
{
    public required string Name { get; set; }
}
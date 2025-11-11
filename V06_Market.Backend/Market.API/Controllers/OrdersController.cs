using Market.Application.Modules.Sales.Orders.Commands.Create;
using Market.Application.Modules.Sales.Orders.Commands.Update;
using Market.Application.Modules.Sales.Orders.Queries.GetById;
using Market.Application.Modules.Sales.Orders.Queries.List;
using Market.Application.Modules.Sales.Orders.Queries.ListWithItems;

namespace Market.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateOrderCommand command, CancellationToken ct)
    {
        int id = await sender.Send(command, ct);

        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, UpdateOrderCommand command, CancellationToken ct)
    {
        // ID from the route takes precedence
        command.Id = id;
        await sender.Send(command, ct);
        // no return -> 204 No Content
    }

    [HttpGet("{id:int}")]
    public async Task<GetByIdOrdersQueryDto> GetById(int id, CancellationToken ct)
    {
        var order = await sender.Send(new GetByIdOrdersQuery { Id = id }, ct);
        return order; // if NotFoundException -> 404 via middleware
    }

    [HttpGet]
    public async Task<PageResult<ListOrdersQueryDto>> List([FromQuery] ListOrdersQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result;
    }

    [HttpGet("WithItems")]
    public async Task<PageResult<ListOrdersWithItemsQueryDto>> ListWithItems([FromQuery] ListOrdersWithItemsQuery query, CancellationToken ct)
    {
        var result = await sender.Send(query, ct);
        return result;
    }
}
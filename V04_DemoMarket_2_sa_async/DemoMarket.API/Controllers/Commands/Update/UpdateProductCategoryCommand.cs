using System.Text.Json.Serialization;

namespace DemoMarket.API.Controllers.Commands.Update;

public sealed class UpdateProductCategoryCommand
{
    public required string Name { get; set; }
}

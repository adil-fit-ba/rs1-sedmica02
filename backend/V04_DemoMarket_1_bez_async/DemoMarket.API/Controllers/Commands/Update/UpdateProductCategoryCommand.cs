using System.Text.Json.Serialization;

namespace DemoMarket.API.Controllers.Commands.Update;

public sealed class UpdateProductCategoryCommand
{
    [JsonIgnore]
    public int Id { get; set; }
    public required string Name { get; set; }
}

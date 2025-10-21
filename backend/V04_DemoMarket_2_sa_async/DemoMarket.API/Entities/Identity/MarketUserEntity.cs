// MarketUserEntity.cs

namespace DemoMarket.API.Entities.Identity;

public sealed class MarketUserEntity
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }
    public bool IsEmployee { get; set; }
    public bool IsEnabled { get; set; }
}
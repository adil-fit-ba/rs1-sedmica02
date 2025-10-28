namespace DemoMarket.Logika.Entities.Catalog;

/// <summary>
/// Represents a product category.
/// </summary>
public class ProductCategoryEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Indicates whether the category is active (enabled).
    /// </summary>
    public bool IsEnabled { get; set; }
}
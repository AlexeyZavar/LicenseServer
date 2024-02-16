namespace LicenseServer.Database.Entities;

public class ProductSetting
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public string? Value { get; set; }
}

namespace LicenseServer.Database.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<ProductFeature> Features { get; set; } = new List<ProductFeature>(0);
    public ICollection<ProductSetting> Settings { get; set; } = new List<ProductSetting>(0);
}

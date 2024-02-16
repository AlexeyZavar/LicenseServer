namespace LicenseServer.Database.Entities;

public class License
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string UserName { get; set; }

    public ICollection<LicenseFeature> Features { get; set; }

    public DateTime Until { get; set; }
}

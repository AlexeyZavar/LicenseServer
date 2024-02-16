namespace LicenseServer.Database.Entities;

public class LicenseFeature
{
    public Guid Id { get; set; }

    public Guid LicenseId { get; set; }
    public License License { get; set; }

    public Guid ProductFeatureId { get; set; }
    public ProductFeature ProductFeature { get; set; }

    public string? Value { get; set; }
}

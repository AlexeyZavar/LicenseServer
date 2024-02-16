#region

using FastEndpoints;
using LicenseServer.Database.Entities;
using LicenseServer.Endpoints.Product;

#endregion

namespace LicenseServer.Mappers;

public class LicenseMapper : Mapper<GetLicenseResponse, GetLicenseResponse, License>
{
    private readonly ProductMapper _productMapper;

    /// <inheritdoc />
    public LicenseMapper(ProductMapper productMapper)
    {
        _productMapper = productMapper;
    }

    /// <inheritdoc />
    public override GetLicenseResponse FromEntity(License e)
    {
        return new GetLicenseResponse
        {
            Id = e.Id,
            Product = _productMapper.FromEntity(e.Product),
            UserName = e.UserName,
            Until = e.Until,
            Features = e.Features.Select(x =>
                                             new GetLicenseFeatureResponse
                                             {
                                                 Id = x.Id,
                                                 Value = x.Value,
                                                 ProductFeature = new GetProductFeatureResponse
                                                 {
                                                     Id = x.ProductFeature.Id,
                                                     Description = x.ProductFeature.Description,
                                                     Name = x.ProductFeature.Name,
                                                     Type = x.ProductFeature.Type,
                                                     DefaultValue = x.ProductFeature.DefaultValue
                                                 }
                                             }
                                        ).ToList()
        };
    }
}

public class GetLicenseResponse
{
    public Guid Id { get; set; }
    public GetProduct Product { get; set; }
    public string UserName { get; set; }
    public ICollection<GetLicenseFeatureResponse> Features { get; set; }
    public DateTime? Until { get; set; }
}

public class GetLicenseFeatureResponse
{
    public Guid Id { get; set; }
    public GetProductFeatureResponse ProductFeature { get; set; }
    public string? Value { get; set; }
}

public class GetProductFeatureResponse
{
    public Guid Id { get; set; }

    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public string? DefaultValue { get; set; }
}

#region

using FastEndpoints;
using LicenseServer.Database.Entities;
using LicenseServer.Endpoints.Product;

#endregion

namespace LicenseServer.Mappers;

public class ProductMapper : Mapper<Product, GetProduct, Product>
{
    /// <inheritdoc />
    public override GetProduct FromEntity(Product e)
    {
        return new GetProduct
        {
            Id = e.Id,
            Name = e.Name,
            Features = e.Features.Select(x => new GetProductFeature
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Type = x.Type,
                DefaultValue = x.DefaultValue
            }).ToList(),
            Settings = e.Settings.Select(x => new GetProductSetting
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                Type = x.Type,
                Value = x.Value
            }).ToList()
        };
    }
}

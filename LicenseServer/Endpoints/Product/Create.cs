#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Endpoints.Product;

public class
    CreateProductEndpoint : EndpointWithMapping<CreateProductRequest, CreateProductResponse, Database.Entities.Product>
{
    private readonly ProductService _productService;

    /// <inheritdoc />
    public CreateProductEndpoint(ProductService productService)
    {
        _productService = productService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/product");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("Root");
        Description(x => x
                         .ProducesProblem(400)
                         .ProducesProblem(401)
                         .ProducesProblem(500));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        var product = MapToEntity(req);
        var res = await _productService.Create(product);

        if (res.IsSuccess)
        {
            Response.Id = res.Value;
        }
        else
        {
            AddError(string.Join('\n', res.Errors));
        }
    }

    /// <inheritdoc />
    public override Database.Entities.Product MapToEntity(CreateProductRequest r)
    {
        var product = new Database.Entities.Product
        {
            Name = r.Name
        };

        var features = r.Features.Select(x =>
                                             new ProductFeature
                                             {
                                                 Product = product,
                                                 Description = x.Description,
                                                 Name = x.Name,
                                                 Type = x.Type,
                                                 DefaultValue = x.DefaultValue
                                             }).ToList();
        var settings = r.Settings.Select(x =>
                                             new ProductSetting
                                             {
                                                 Product = product,
                                                 Description = x.Description,
                                                 Name = x.Name,
                                                 Type = x.Type,
                                                 Value = x.Value
                                             }).ToList();

        product.Features = features;
        product.Settings = settings;

        return product;
    }
}

public class CreateProductRequest
{
    public string Name { get; set; }
    public ICollection<CreateProductFeature> Features { get; set; }
    public ICollection<CreateProductSetting> Settings { get; set; }
}

public class CreateProductFeature
{
    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string? DefaultValue { get; set; }
}

public class CreateProductSetting
{
    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string? Value { get; set; }
}

public class CreateProductResponse
{
    public Guid Id { get; set; }
}

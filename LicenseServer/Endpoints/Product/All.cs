#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Mappers;

#endregion

namespace LicenseServer.Endpoints.Product;

public class All : EndpointWithoutRequest<AllProductsResponse>
{
    private readonly ProductMapper _mapper;
    private readonly ProductService _productService;

    /// <inheritdoc />
    public All(ProductService productService, ProductMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("/product/all");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("Root");
        Description(x => x
                        .ProducesProblem(401));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CancellationToken ct)
    {
        var res = await _productService.GetAll();

        Response.Products = res.Value.Select(_mapper.FromEntity).ToList();
    }
}

public class AllProductsResponse
{
    public ICollection<GetProduct> Products { get; set; }
}

public class GetProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<GetProductFeature> Features { get; set; } = new List<GetProductFeature>(0);
    public ICollection<GetProductSetting> Settings { get; set; } = new List<GetProductSetting>(0);
}

public class GetProductFeature
{
    public Guid Id { get; set; }

    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public string? DefaultValue { get; set; }
}

public class GetProductSetting
{
    public Guid Id { get; set; }

    public string Description { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }

    public string? Value { get; set; }
}

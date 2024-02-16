#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Endpoints.License;

public class
    CreateLicenseEndpoint : EndpointWithMapping<CreateLicenseRequest, CreateLicenseResponse, Database.Entities.License>
{
    private readonly LicenseService _licenseService;

    /// <inheritdoc />
    public CreateLicenseEndpoint(LicenseService licenseService)
    {
        _licenseService = licenseService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/license");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("Root");
        Description(x => x
                         .ProducesProblem(400)
                         .ProducesProblem(401));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateLicenseRequest req, CancellationToken ct)
    {
        var license = MapToEntity(req);
        var res = await _licenseService.Create(license);

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
    public override Database.Entities.License MapToEntity(CreateLicenseRequest r)
    {
        return new Database.Entities.License
        {
            ProductId = r.ProductId,
            UserName = r.UserName,
            Features = r.Features.Select(x =>
                                             new LicenseFeature
                                             {
                                                 ProductFeatureId = x.ProductFeatureId,
                                                 Value = x.Value
                                             }).ToList(),
            Until = r.Until
        };
    }
}

public class CreateLicenseRequest
{
    public Guid ProductId { get; set; }
    public string UserName { get; set; }
    public ICollection<CreateLicenseFeature> Features { get; set; }

    public DateTime Until { get; set; }
}

public class CreateLicenseFeature
{
    public Guid ProductFeatureId { get; set; }

    public string? Value { get; set; }
}

public class CreateLicenseResponse
{
    public Guid Id { get; set; }
}

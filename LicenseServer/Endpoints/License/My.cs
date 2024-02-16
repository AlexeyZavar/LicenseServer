#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Mappers;

#endregion

namespace LicenseServer.Endpoints.License;

public class MyLicenseEndpoint : Endpoint<MyLicenseRequest, GetLicenseResponse>
{
    private readonly LicenseMapper _licenseMapper;
    private readonly LicenseService _licenseService;

    /// <inheritdoc />
    public MyLicenseEndpoint(LicenseService licenseService, LicenseMapper licenseMapper)
    {
        _licenseService = licenseService;
        _licenseMapper = licenseMapper;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("/license/my");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Description(x => x
                        .ProducesProblem(401));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(MyLicenseRequest req, CancellationToken ct)
    {
        var license = await _licenseService.Get(req.LicenseId);
        var response = _licenseMapper.FromEntity(license);

        await SendAsync(response, cancellation: ct);
    }
}

public class MyLicenseRequest
{
    [FromClaim(XClaimTypes.Id)] public Guid LicenseId { get; set; }
}

#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Mappers;

#endregion

namespace LicenseServer.Endpoints.License;

public class Get : Endpoint<GetLicenseRequest, GetLicenseResponse>
{
    private readonly LicenseMapper _licenseMapper;
    private readonly LicenseService _licenseService;

    /// <inheritdoc />
    public Get(LicenseService licenseService, LicenseMapper licenseMapper)
    {
        _licenseService = licenseService;
        _licenseMapper = licenseMapper;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("/license/{Id}");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("Root");
        Description(x => x
                         .ProducesProblem(400)
                         .ProducesProblem(401)
                         .ProducesProblem(404));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(GetLicenseRequest req, CancellationToken ct)
    {
        var license = await _licenseService.Get(req.Id);

        if (!license.IsSuccess)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = _licenseMapper.FromEntity(license);

        await SendAsync(response, cancellation: ct);
    }
}

public class GetLicenseRequest
{
    public Guid Id { get; set; }
}

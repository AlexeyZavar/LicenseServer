#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;

#endregion

namespace LicenseServer.Endpoints.File;

public class Get : Endpoint<GetFileRequest, GetFileResponse>
{
    private readonly FileService _fileService;
    private readonly LicenseService _licenseService;

    /// <inheritdoc />
    public Get(FileService fileService, LicenseService licenseService)
    {
        _fileService = fileService;
        _licenseService = licenseService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Get("/file/{Name}");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("NotExpired");
        Description(x => x
                         .Produces(200, null, "application/octet-stream")
                         .ProducesProblem(401)
                         .ProducesProblem(404)
                   );
    }

    /// <inheritdoc />
    public override async Task HandleAsync(GetFileRequest req, CancellationToken ct)
    {
        var licenseResult = await _licenseService.Get(req.LicenseId);
        var license = licenseResult.Value;

        var fileResult = await _fileService.GetByName(req.Name).ConfigureAwait(false);

        if (!fileResult.IsSuccess)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var file = fileResult.Value;

        if (file.ProductId != license?.ProductId)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var f = System.IO.File.OpenRead(file.Path);

        await SendStreamAsync(f, file.Name, f.Length, cancellation: ct);
    }
}

public class GetFileRequest
{
    [FromClaim(XClaimTypes.Id)] public Guid LicenseId { get; set; }

    public string Name { get; set; }
}

public class GetFileResponse
{
}

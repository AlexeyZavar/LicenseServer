#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;

#endregion

namespace LicenseServer.Endpoints.File;

public class Update : Endpoint<UpdateFileRequest>
{
    private readonly FileService _fileService;

    /// <inheritdoc />
    public Update(FileService fileService)
    {
        _fileService = fileService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/file/{Id}");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("Root");
        AllowFileUploads();
        Description(x => x
                         .ProducesProblem(401)
                         .ProducesProblem(404));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(UpdateFileRequest req, CancellationToken ct)
    {
        var file = await _fileService.GetById(req.Id);

        if (!file.IsSuccess)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await using var f = System.IO.File.Open(file.Value.Path, FileMode.Create, FileAccess.Write);
        await req.File.CopyToAsync(f, ct);

        await SendOkAsync(ct);
    }
}

public class UpdateFileRequest
{
    public Guid Id { get; set; }
    public IFormFile File { get; set; }
}

#region

using FastEndpoints;
using LicenseServer.Authentication;
using LicenseServer.Core.Services;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Endpoints.File;

public class Create : Endpoint<CreateFileRequest, CreateFileResponse>
{
    private readonly FileService _fileService;

    /// <inheritdoc />
    public Create(FileService fileService)
    {
        _fileService = fileService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/file");
        AuthSchemes(XAuthSchemeConstants.SchemeName);
        Permissions("Root");
        AllowFileUploads();
        Description(x => x
                         .ProducesProblem(401)
                         .ProducesProblem(500));
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateFileRequest req, CancellationToken ct)
    {
        var dir = Path.Combine(Env.ContentRootPath, "Files");
        var path = Path.Combine(dir, req.Name);

        Directory.CreateDirectory(dir);

        await using var f = System.IO.File.OpenWrite(path);
        await req.File.CopyToAsync(f, ct);

        var productFile = new ProductFile
        {
            ProductId = req.ProductId,
            Name = req.Name,
            Path = path
        };

        var res = await _fileService.Create(productFile);

        Response.Id = res.Id;
    }
}

public class CreateFileRequest
{
    public IFormFile File { get; set; }
    public string Name { get; set; }
    public Guid ProductId { get; set; }
}

public class CreateFileResponse
{
    public Guid Id { get; set; }
}

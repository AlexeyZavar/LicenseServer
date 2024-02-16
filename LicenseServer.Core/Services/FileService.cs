#region

using Ardalis.Result;
using LicenseServer.Core.Specifications;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Services;

public class FileService
{
    private readonly EntityRepository<ProductFile> _repository;

    public FileService(EntityRepository<ProductFile> repository)
    {
        _repository = repository;
    }

    public async Task<ProductFile> Create(ProductFile productFile)
    {
        await _repository.AddAsync(productFile);
        await _repository.SaveChangesAsync();

        return productFile;
    }

    public async Task<Result<ProductFile>> GetById(Guid id)
    {
        var spec = new FileByIdSpec(id);
        var res = await _repository.GetBySpecAsync(spec);

        if (res == null)
        {
            return Result<ProductFile>.NotFound();
        }

        return Result<ProductFile>.Success(res);
    }

    public async Task<Result<ProductFile>> GetByName(string name)
    {
        var spec = new FileByNameSpec(name);
        var res = await _repository.GetBySpecAsync(spec);

        if (res == null)
        {
            return Result<ProductFile>.NotFound();
        }

        return Result<ProductFile>.Success(res);
    }
}

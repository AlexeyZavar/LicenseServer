#region

using Ardalis.Result;
using LicenseServer.Core.Specifications;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Services;

public class LicenseService
{
    private readonly EntityRepository<License> _repository;

    public LicenseService(EntityRepository<License> repository)
    {
        _repository = repository;
    }

    public async Task<Result<License>> Get(Guid id)
    {
        var spec = new LicenseByIdSpec(id);
        var res = await _repository.GetBySpecAsync(spec);

        if (res == null)
        {
            return Result<License>.NotFound();
        }

        return Result<License>.Success(res);
    }

    public async Task<Result<Guid>> Create(License license)
    {
        await _repository.AddAsync(license);
        await _repository.SaveChangesAsync();

        return Result<Guid>.Success(license.Id);
    }
}

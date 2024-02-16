#region

using Ardalis.Result;
using LicenseServer.Core.Specifications;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Services;

public class ProductService
{
    private readonly EntityRepository<Product> _repository;

    public ProductService(EntityRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Create(Product product)
    {
        var spec = new ProductByNameSpec(product.Name);
        var found = await _repository.AnyAsync(spec);

        if (found)
        {
            return Result<Guid>.Error("Product with this name already exists");
        }

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();

        return Result<Guid>.Success(product.Id);
    }

    public async Task<Result<Product>> Get(Guid id)
    {
        var spec = new ProductByIdSpec(id);
        var product = await _repository.GetBySpecAsync(spec);

        if (product == null)
        {
            return Result<Product>.NotFound();
        }

        return Result<Product>.Success(product);
    }

    public async Task<Result<List<Product>>> GetAll()
    {
        var res = await _repository.ListAsync();

        return Result<List<Product>>.Success(res);
    }
}

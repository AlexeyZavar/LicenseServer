#region

using Ardalis.Specification;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Specifications;

public sealed class ProductByIdSpec : Specification<Product>, ISingleResultSpecification<Product>
{
    public ProductByIdSpec(Guid id)
    {
        Query.Where(x => x.Id == id).Include(x => x.Settings).Include(x => x.Features);
    }
}

#region

using Ardalis.Specification;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Specifications;

public class FileByIdSpec : Specification<ProductFile>, ISingleResultSpecification<ProductFile>
{
    /// <inheritdoc />
    public FileByIdSpec(Guid id)
    {
        Query.Where(x => x.Id == id).Include(x => x.Product);
    }
}

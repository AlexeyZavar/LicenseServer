#region

using Ardalis.Specification;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Specifications;

public class FileByNameSpec : Specification<ProductFile>, ISingleResultSpecification<ProductFile>
{
    /// <inheritdoc />
    public FileByNameSpec(string name)
    {
        Query.Where(x => x.Name == name).Include(x => x.Product);
    }
}

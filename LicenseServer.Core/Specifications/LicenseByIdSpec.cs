#region

using Ardalis.Specification;
using LicenseServer.Database.Entities;

#endregion

namespace LicenseServer.Core.Specifications;

public class LicenseByIdSpec : Specification<License>, ISingleResultSpecification<License>
{
    public LicenseByIdSpec(Guid id)
    {
        Query
            .Where(x => x.Id == id)
            .Include(x => x.Features)
            .ThenInclude(x => x.ProductFeature)
            .Include(x => x.Product)
            .ThenInclude(x => x.Features)
            .Include(x => x.Product)
            .ThenInclude(x => x.Settings);
    }
}

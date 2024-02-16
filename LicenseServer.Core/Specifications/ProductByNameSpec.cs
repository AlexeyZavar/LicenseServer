#region

using Ardalis.Specification;
using LicenseServer.Database.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace LicenseServer.Core.Specifications;

public sealed class ProductByNameSpec : Specification<Product>
{
    public ProductByNameSpec(string name)
    {
        Query.Where(x => EF.Functions.Like(x.Name, name));
    }
}

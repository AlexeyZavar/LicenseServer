#region

using Ardalis.Specification.EntityFrameworkCore;
using LicenseServer.Database;

#endregion

namespace LicenseServer.Core;

public class EntityRepository<T> : RepositoryBase<T> where T : class
{
    private readonly XContext _context;

    /// <inheritdoc />
    public EntityRepository(XContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
}

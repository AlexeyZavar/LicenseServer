#region

using LicenseServer.Database.Entities;
using Microsoft.EntityFrameworkCore;

#endregion

namespace LicenseServer.Database;

public class XContext : DbContext
{
    /// <inheritdoc />
    public XContext(DbContextOptions<XContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductFeature> ProductFeatures { get; set; } = null!;
    public DbSet<ProductSetting> ProductSettings { get; set; } = null!;
    public DbSet<ProductFile> ProductFiles { get; set; } = null!;

    public DbSet<License> Licenses { get; set; } = null!;
    public DbSet<LicenseFeature> LicenseFeatures { get; set; } = null!;

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductFile>()
                    .HasIndex(x => x.Name)
                    .IsUnique();
    }
}

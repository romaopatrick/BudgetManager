using GODCommon.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GODProducts.Infra;

public sealed class DefaultContext : DefaultContextBase<Product>
{
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
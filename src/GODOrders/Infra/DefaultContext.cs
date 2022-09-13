using GODCommon.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GODOrders.Infra;

public sealed class DefaultContext : DefaultContextBase<DefaultContext, Order>
{
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
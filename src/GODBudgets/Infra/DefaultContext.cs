using GODCommon.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GODBudgets.Infra;

public sealed class DefaultContext : DefaultContextBase<Budget>
{
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {}

    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
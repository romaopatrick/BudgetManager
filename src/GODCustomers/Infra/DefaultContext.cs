using GODCommon.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GODCustomers.Infra;

public sealed class DefaultContext: DefaultContextBase<DefaultContext, Customer>
{
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Event> Events => Set<Event>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DefaultContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

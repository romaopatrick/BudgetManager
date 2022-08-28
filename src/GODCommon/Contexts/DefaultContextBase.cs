using FastEndpoints;
using GODCommon.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GODCommon.Contexts;

public abstract class DefaultContextBase<TContext, TSnapshot> : DbContext where TContext : DbContext
{
    public DefaultContextBase(DbContextOptions<TContext> options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.LazyLoadingEnabled = false;
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
            if (entry.Entity is EntityBase.AsSnapshot snapshot) HandleSnapshot(entry, snapshot);
            else if(entry.Entity is EventEntity<TSnapshot> @event) HandleEvent(entry, @event);

        return base.SaveChangesAsync(cancellationToken);
    }
    private void HandleEvent(EntityEntry<EntityBase> entry, EventEntity<TSnapshot> entity)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                entity.CreatedAt = DateTime.UtcNow;
                break;

            case EntityState.Modified:
                entity.UpdatedAt = DateTime.UtcNow;
                break;
        }
    }

    private static void HandleSnapshot(EntityEntry<EntityBase> entry, EntityBase.AsSnapshot entity)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entity.Enabled = true;
                break;

            case EntityState.Modified:
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entity.Version++;
                break;
        }
    }
}
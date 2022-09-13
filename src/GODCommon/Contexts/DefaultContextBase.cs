using System.Diagnostics.CodeAnalysis;
using GODCommon.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GODCommon.Contexts;

public abstract class DefaultContextBase<TSnapshot> : DbContext
    where TSnapshot : EntityBase.AsSnapshot
{
    [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
    protected DefaultContextBase(DbContextOptions options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.LazyLoadingEnabled = false;
    }

    public async Task SaveEventAsync<TEvent>(TSnapshot snapshot, TEvent @event, CancellationToken ct)
    where TEvent : EventEntity<TSnapshot>
    {
        Set<TSnapshot>().Update(snapshot);
        await Set<TEvent>().AddAsync(@event, ct);

        await SaveChangesAsync(ct);
    }
    public async Task SaveEventAsync<TEvent>(TEvent @event, CancellationToken ct)
        where TEvent : EventEntity<TSnapshot>
    {
        await Set<TEvent>().AddAsync(@event, ct);
        await SaveChangesAsync(ct);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
            if (entry.Entity is EntityBase.AsSnapshot snapshot) HandleSnapshot(entry, snapshot);
            else if(entry.Entity is EventEntity<TSnapshot> @event) HandleEvent(entry, @event);

        return base.SaveChangesAsync(cancellationToken);
    }
    private static void HandleEvent(EntityEntry<EntityBase> entry, EventEntity<TSnapshot> entity)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                entity.CreatedAt = DateTime.UtcNow;
                break;
        }
    }

    private static void HandleSnapshot(EntityEntry<EntityBase> entry, EntityBase.AsSnapshot entity)
    {
        switch (entry.State)
        {
            case EntityState.Added:
                entity.CreatedAt = DateTime.UtcNow;
                entity.Enabled = true;
                break;

            case EntityState.Modified:
                entity.UpdatedAt = DateTime.UtcNow;
                entity.Version++;
                break;
        }
    }
}
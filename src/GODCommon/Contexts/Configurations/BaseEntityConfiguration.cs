using GODCommon.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GODCommon.Contexts.Configurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedAt).ValueGeneratedOnUpdate();
    }
    public abstract class AsSnapshotConfiguration<TSnapshot> : BaseEntityConfiguration<TSnapshot> where TSnapshot : EntityBase.AsSnapshot
    {
        public override void Configure(EntityTypeBuilder<TSnapshot> builder)
        {
            builder.HasQueryFilter(x => x.Enabled);
            builder.HasAlternateKey(x => x.SnapshotNumber);
            builder.Property(x => x.SnapshotNumber).ValueGeneratedOnAdd();
            base.Configure(builder);
        }
    }
}
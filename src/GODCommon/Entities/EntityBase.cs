namespace GODCommon.Entities;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public abstract class AsSnapshot : EntityBase
    {
        public long SnapshotNumber { get; set; }
        public AsSnapshot() => Enabled = true;
        public bool Enabled { get; set; }
        public int Version { get; set; }
    }
}
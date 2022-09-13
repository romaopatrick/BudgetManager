using System.ComponentModel.DataAnnotations.Schema;
using GODCommon.Enums;

namespace GODCommon.Entities;

public abstract class EventEntity<TSnapshot> : EntityBase
{
    public Guid CreatedBy { get; set; }
    public string? CreatedByEmail { get; set; }
    [Column(TypeName = "jsonb")] public TSnapshot? Snapshot { get; set; }
    public EventType Type { get; set; }
}
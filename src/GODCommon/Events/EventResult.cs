using GODCommon.Entities;
using GODCommon.Enums;

namespace GODCommon.Events;

public sealed class EventResult<TSnapshot> where TSnapshot : EntityBase.AsSnapshot 
{
    public Guid EventId { get; init; }
    public DateTime EventDate { get; init; }
    public TSnapshot? Snapshot { get; init; }
    public EventType Type { get; init; }
}

public static class EventResultTrigger
{
    public static EventResult<TSnapshot> Trigger<TSnapshot>(EventEntity<TSnapshot> bc)
        where TSnapshot : EntityBase.AsSnapshot 
        => new()
        {
            Snapshot = bc.Snapshot,
            EventId = bc.Id,
            EventDate = bc.CreatedAt!.Value,
            Type = bc.Type
        };
}
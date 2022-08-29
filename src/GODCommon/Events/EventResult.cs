using GODCommon.Entities;
using GODCommon.Enums;

namespace GODCommon.Events;

public class EventResult<TSnapshot> where TSnapshot : EntityBase.AsSnapshot 
{
    public Guid EventId { get; init; }
    public DateTime EventDate { get; init; }
    public TSnapshot? Snapshot { get; init; }
    public EventType Type { get; init; }

    public static EventResult<TSnapshot> Trigger(EventEntity<TSnapshot> bc)
        => new()
        {
            Snapshot = bc.Snapshot,
            EventId = bc.Id,
            EventDate = bc.CreatedAt!.Value,
            Type = bc.Type
        };
}
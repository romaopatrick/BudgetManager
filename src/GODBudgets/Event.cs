using GODCommon.Entities;
using GODCommon.Enums;

namespace GODBudgets;

public sealed class Event : EventEntity<Budget>
{
    public static Event Trigger(Budget s, EventType type) => new()
    {
        Snapshot = s,
        Type = type
    };
}
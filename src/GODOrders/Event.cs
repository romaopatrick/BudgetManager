using GODCommon.Entities;
using GODCommon.Enums;

namespace GODOrders;

public sealed class Event : EventEntity<Order>
{
    public static Event Trigger(Order s, EventType type) => new()
    {
        Snapshot = s,
        Type = type
    };
}
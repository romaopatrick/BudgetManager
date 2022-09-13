using GODCommon.Entities;
using GODCommon.Enums;

namespace GODCustomers;

public sealed class Event : EventEntity<Customer>
{
    public static Event Trigger(Customer s, EventType type) => new()
    {
        Snapshot = s,
        Type = type
    };
}
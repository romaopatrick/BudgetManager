using GODCommon.Entities;
using GODCommon.Enums;

namespace GODProducts;

public sealed class Event : EventEntity<Product>
{
    public static Event Trigger(Product s, EventType t) => new()
    {
        Snapshot = s,
        Type = t
    };
}
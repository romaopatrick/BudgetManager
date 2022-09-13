using GODCommon.Entities;
using GODCommon.Enums;

namespace GODOrders;

public sealed class Order : EntityBase.AsSnapshot
{
    public long CustomerNumber { get; set; }
    public long ProductNumber { get; set; }
    public string? Details { get; set; }
    public OrderStatus Status { get; set; }
}
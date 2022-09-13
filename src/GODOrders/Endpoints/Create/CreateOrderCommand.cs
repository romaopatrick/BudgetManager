using GODCommon.Enums;

namespace GODOrders.Endpoints.Create;

public sealed class CreateOrderCommand
{
    public long ProductNumber { get; init; }
    public long CustomerNumber { get; init; }
    public string? Details { get; init; }

    public Order ToEntity() => new()
    {
        Details = Details,
        Status = OrderStatus.Open,
        CustomerNumber = CustomerNumber,
        ProductNumber = ProductNumber
    };
}
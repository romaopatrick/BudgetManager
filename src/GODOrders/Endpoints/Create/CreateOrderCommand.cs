using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODOrders.Endpoints.Create;

public class CreateOrderCommand : IRequest<IResult<EventResult<Order>>>
{
    public long? ProductNumber { get; init; }
    public long? CustomerNumber { get; init; }
    public string? Details { get; init; }

    public Order ToEntity() => new()
    {
        Details = Details,
        Status = OrderStatus.Open,
        CustomerNumber = CustomerNumber!.Value,
        ProductNumber = ProductNumber!.Value
    };
}
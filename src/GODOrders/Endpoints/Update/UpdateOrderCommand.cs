using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODOrders.Endpoints.Update;

public sealed class UpdateOrderCommand : IRequest<IResult<EventResult<Order>>>
{
    public Guid OrderId { get; init; }
    public string? Details { get; init; }

    public void UpdateEntity(Order order)
    {
        if (Details is not null)
            order.Details = Details;
    }
}
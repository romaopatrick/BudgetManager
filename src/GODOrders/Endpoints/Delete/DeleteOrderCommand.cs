using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODOrders.Endpoints.Delete;

public sealed class DeleteOrderCommand : IRequest<EventResult<Order>>, IRequest<IResult<EventResult<Order>>>
{
    public Guid OrderId { get; init; }
}
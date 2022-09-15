using GODCommon.Enums;
using GODCommon.Results;
using GODCommon.Results.Paging;
using MediatR;

namespace GODOrders.Endpoints.Retrieve;

public sealed class RetrieveOrdersCommand : Paged, IRequest<IResult<Paged<Order>>>
{
    public long? CustomerNumber { get; init; }
    public long? ProductNumber { get; init; }
    public OrderStatus? Status { get; init; }
}
using GODCommon.Results;
using MediatR;

namespace GODOrders.Endpoints.Detail;

public sealed class DetailOrderCommand : IRequest<IResult<Order>>
{
    public long SnapshotNumber { get; init; }
}
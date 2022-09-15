using GODCommon.Results;
using MediatR;

namespace GODCustomers.Endpoints.Detail;

public sealed class DetailCustomerCommand : IRequest<IResult<Customer>>
{
    public long SnapshotNumber { get; init; }
}
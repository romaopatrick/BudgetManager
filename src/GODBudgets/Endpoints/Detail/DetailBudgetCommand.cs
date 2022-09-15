using GODCommon.Results;
using MediatR;

namespace GODBudgets.Endpoints.Detail;

public sealed class DetailBudgetCommand : IRequest<IResult<Budget>>
{
    public long SnapshotNumber { get; init; }
}
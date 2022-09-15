using GODCommon.Results;
using MediatR;

namespace GODProducts.Endpoints.Detail;

public sealed class DetailProductCommand : IRequest<IResult<Product>>
{
    public long SnapshotNumber { get; init; }
}
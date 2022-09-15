using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODProducts.Endpoints.Delete;

public class DeleteProductCommand : IRequest<IResult<EventResult<Product>>>
{
    public Guid ProductId { get; init; }
}
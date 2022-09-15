using System.Net;
using GODCommon.Endpoints;
using GODCommon.Events;
using GODCommon.Results;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Delete;

public sealed class DeleteOrderEndpoint : BaseEndpoint<DeleteOrderCommand, EventResult<Order>>
{

    public override void Configure()
    {
        Delete("deletions/{orderId}");
        Description(c => c.Produces<IResult<EventResult<Order>>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public DeleteOrderEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
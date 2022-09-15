using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODOrders.Infra;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Create;

public sealed class CreateOrderEndpoint : BaseEndpoint<CreateOrderCommand, EventResult<Order>>
{
    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Order>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public CreateOrderEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
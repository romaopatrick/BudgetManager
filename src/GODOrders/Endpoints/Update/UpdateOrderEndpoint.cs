using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Update;

public sealed class UpdateOrderEndpoint : BaseEndpoint<UpdateOrderCommand, EventResult<Order>>
{
    
    public override void Configure()
    {
        Put("updates/{orderId}");
        Description(c => c.Produces<IResult<EventResult<Order>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }
    public UpdateOrderEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
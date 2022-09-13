using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Update;

public sealed class UpdateOrderEndpoint : BaseEndpoint<UpdateOrderCommand, EventResult<Order>>
{
    public UpdateOrderEndpoint(DefaultContext context) : base(context)
    {
    }
    
    public override void Configure()
    {
        Put("updates/{orderId}");
        Description(c => c.Produces<IResult<EventResult<Order>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateOrderCommand req, CancellationToken ct)
    {
        var order = await Context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);
        if (order is null)
            await SendAsync(RFac.WithError<EventResult<Order>>(OrderNotifications.OrderNotFound),
                (int)HttpStatusCode.NotFound, ct);
        else
        {
            req.UpdateEntity(order);
            var update = Event.Trigger(order, EventType.Update);
            
            await Context.SaveEventAsync(order, update, ct);
            await SendAsync(
                RFac.WithSuccess(EventResultTrigger.Trigger(update)), cancellation: ct);
        }
    }

 
}
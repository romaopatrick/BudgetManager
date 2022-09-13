using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Delete;

public sealed class DeleteOrderEndpoint : BaseEndpoint<DeleteOrderCommand, EventResult<Order>>
{
    public DeleteOrderEndpoint(DefaultContext context) : base(context){}

    public override void Configure()
    {
        Delete("deletions/{orderId}");
        Description(c => c.Produces<IResult<EventResult<Order>>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteOrderCommand req, CancellationToken ct)
    {
        var order = await Context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);
        if (order is null)
            await SendAsync(RFac.WithError<EventResult<Order>>(OrderNotifications.OrderNotFound),
                (int)HttpStatusCode.NotFound, ct);

        else
        {
            Context.Orders.Remove(order);
            var deletion = Event.Trigger(order, EventType.Deletion);
            await Context.SaveEventAsync(deletion, ct);

            await SendAsync(RFac.WithSuccess(EventResultTrigger.Trigger(deletion)), cancellation: ct);
        }
    }
}
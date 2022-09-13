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
    private readonly DefaultContext _context;
    public DeleteOrderEndpoint(DefaultContext context) => _context = context;

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
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);
        if (order is null)
            await SendAsync(RFac.WithError<EventResult<Order>>(OrderNotifications.OrderNotFound),
                (int)HttpStatusCode.NotFound, ct);

        else
        {
            _context.Orders.Remove(order);
            var deletion = Event.Trigger(order, EventType.Deletion);
            await _context.SaveEventAsync(deletion, ct);

            await SendAsync(RFac.WithSuccess(EventResult<Order>.Trigger(deletion)), cancellation: ct);
        }
    }
}
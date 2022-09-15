using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODOrders.Endpoints.Delete;

public class DeleteOrderHandler : BaseHandler<DeleteOrderCommand, EventResult<Order>>
{
    public DeleteOrderHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Order>>> Handle(DeleteOrderCommand req, CancellationToken ct)
    {
        var order = await Context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);
        
        if (order is null) return RFac.WithError<EventResult<Order>>(
                OrderNotifications.OrderNotFound, HttpStatusCode.NotFound);

        Context.Orders.Remove(order);
        var deletion = Event.Trigger(order, EventType.Deletion);
        await Context.SaveEventAsync(deletion, ct);

        return RFac.WithSuccess(EventResultTrigger.Trigger(deletion));
    }
}
using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODOrders.Endpoints.Update;

public class UpdateOrderHandler : BaseHandler<UpdateOrderCommand, EventResult<Order>>
{
    public UpdateOrderHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Order>>> Handle(UpdateOrderCommand req, CancellationToken ct)
    {
        var order = await Context.Orders.FirstOrDefaultAsync(x => x.Id == req.OrderId, ct);
        if (order is null) return RFac.WithError<EventResult<Order>>(
                OrderNotifications.OrderNotFound, HttpStatusCode.NotFound);
        req.UpdateEntity(order);
        var update = Event.Trigger(order, EventType.Update);

        await Context.SaveEventAsync(order, update, ct);
        return RFac.WithSuccess(EventResultTrigger.Trigger(update));
    }
}
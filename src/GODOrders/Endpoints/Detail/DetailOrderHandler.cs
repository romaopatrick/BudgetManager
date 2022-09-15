using System.Net;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODOrders.Endpoints.Detail;

public class DetailOrderHandler : BaseHandler<DetailOrderCommand, Order>
{
    public DetailOrderHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Order>> Handle(DetailOrderCommand req, CancellationToken ct)
    {
        var order = await Context.Orders.FirstOrDefaultAsync(x => x.SnapshotNumber == req.SnapshotNumber, ct);
        return order is null 
            ? RFac.WithError<Order>(OrderNotifications.OrderNotFound, HttpStatusCode.NotFound)
            : RFac.WithSuccess(order);
    }
}
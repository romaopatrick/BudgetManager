using System.Net;
using GODCommon.Endpoints;
using GODCommon.Notifications;
using GODCommon.Results;
using GODOrders.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Detail;

public sealed class DetailOrderEndpoint : BaseEndpoint<DetailOrderCommand, Order>
{
    private readonly DefaultContext _context;

    public DetailOrderEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Order>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DetailOrderCommand req, CancellationToken ct)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(b => b.SnapshotNumber == req.SnapshotNumber, ct);
        if (order is null)
            await SendAsync(RFac.WithError<Order>(OrderNotifications.OrderNotFound),
            (int)HttpStatusCode.NotFound, ct);
        
        else await SendAsync(RFac.WithSuccess(order), cancellation: ct);
    }
}
using System.Net;
using GODCommon.Endpoints;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Detail;

public sealed class DetailCustomerEndpoint : BaseEndpoint<DetailCustomerCommand, Customer>
{
    private readonly DefaultContext _context;

    public DetailCustomerEndpoint(DefaultContext context) => _context = context;
    
    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Customer>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DetailCustomerCommand req, CancellationToken ct)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(b => b.SnapshotNumber == req.SnapshotNumber, ct);
        if (customer is null)
            await SendAsync(
                RFac.WithError<Customer>(CustomerNotifications.CustomerNotFound),
                (int)HttpStatusCode.NotFound, ct);
        
        else await SendAsync(RFac.WithSuccess(customer), cancellation: ct);
    }
}
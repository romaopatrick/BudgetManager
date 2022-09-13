using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Delete;

public class DeleteCustomerEndpoint : BaseEndpoint<DeleteCustomerCommand, EventResult<Customer>>
{
    private readonly DefaultContext _context;
    public DeleteCustomerEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Delete("deletions/{customerId}");
        Description(c => c.Produces<IResult<EventResult<Customer>>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteCustomerCommand req, CancellationToken ct)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == req.CustomerId, ct);
        if (customer is null)
            await SendAsync(
                RFac.WithError<EventResult<Customer>>(CustomerNotifications.CustomerNotFound),
                (int)HttpStatusCode.NotFound, ct);
        else
        {
            _context.Remove(customer);
            var deletion = Event.Trigger(customer, EventType.Deletion);

            await _context.SaveEventAsync(deletion, ct);
            await SendAsync(RFac.WithSuccess(EventResult<Customer>.Trigger(deletion)), cancellation: ct);
        }
    }
}
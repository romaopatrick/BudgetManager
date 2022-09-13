using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Update;

public sealed class UpdateCustomerEndpoint : BaseEndpoint<UpdateCustomerCommand, EventResult<Customer>>
{
    private readonly DefaultContext _context;
    public UpdateCustomerEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Put("updates/{customerId}");
        Description(c => c.Produces<IResult<EventResult<Customer>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCustomerCommand req, CancellationToken ct)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == req.CustomerId, ct);
        if (customer is null)
            await SendAsync(
                RFac.WithError<EventResult<Customer>>(
                    CustomerNotifications.CustomerNotFound), 
                    (int)HttpStatusCode.NotFound, ct);
        else
        {
            req.UpdateEntity(customer);
            var update = Event.Trigger(customer, EventType.Update);

            await _context.SaveEventAsync(customer, update, ct);
            await SendAsync(RFac.WithSuccess(EventResult<Customer>.Trigger(update)), cancellation: ct);
        }
    }
}
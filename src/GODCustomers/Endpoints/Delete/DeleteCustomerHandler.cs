using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODCustomers.Endpoints.Delete;

public class DeleteCustomerHandler : BaseHandler<DeleteCustomerCommand, EventResult<Customer>>
{
    public DeleteCustomerHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Customer>>> Handle(DeleteCustomerCommand req, CancellationToken ct)
    {
        var customer = await Context.Customers.FirstOrDefaultAsync(x => x.Id == req.CustomerId, ct);
        if (customer is null) return RFac.WithError<EventResult<Customer>>(
            CustomerNotifications.CustomerNotFound, HttpStatusCode.NotFound);
        
        Context.Remove(customer);
        var deletion = Event.Trigger(customer, EventType.Deletion);

        await Context.SaveEventAsync(deletion, ct);
        return RFac.WithSuccess(EventResultTrigger.Trigger(deletion));
    }
}
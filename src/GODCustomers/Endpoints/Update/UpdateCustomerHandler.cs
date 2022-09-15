using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODCustomers.Endpoints.Update;

public class UpdateCustomerHandler : BaseHandler<UpdateCustomerCommand, EventResult<Customer>>
{
    public UpdateCustomerHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Customer>>> Handle(UpdateCustomerCommand req, CancellationToken ct)
    {
        var customer = await Context.Customers.FirstOrDefaultAsync(x => x.Id == req.CustomerId, ct);
        if (customer is null) return RFac.WithError<EventResult<Customer>>(
            CustomerNotifications.CustomerNotFound, HttpStatusCode.NotFound);
        
        req.UpdateEntity(customer);
        var update = Event.Trigger(customer, EventType.Update);

        await Context.SaveEventAsync(customer, update, ct);
        return RFac.WithSuccess(EventResultTrigger.Trigger(update));
    }
}
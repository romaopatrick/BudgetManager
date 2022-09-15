using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODCustomers.Infra;

namespace GODCustomers.Endpoints.Create;

public class CreateCustomerHandler : BaseHandler<CreateCustomerCommand, EventResult<Customer>>
{
    public CreateCustomerHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Customer>>> Handle(CreateCustomerCommand req, CancellationToken ct)
    {
        var customer = req.ToEntity();
        var creation = Event.Trigger(customer, EventType.Creation);

        await Context.SaveEventAsync(customer, creation, ct);
        return RFac.WithSuccess(EventResultTrigger.Trigger(creation), HttpStatusCode.Created);
    }
}
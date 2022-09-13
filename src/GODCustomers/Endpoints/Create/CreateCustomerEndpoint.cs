using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODCustomers.Infra;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Create;

public sealed class CreateCustomerEndpoint : BaseEndpoint<CreateCustomerCommand, EventResult<Customer>>
{
    public CreateCustomerEndpoint(DefaultContext context) : base(context) {}
    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Customer>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCustomerCommand req, CancellationToken ct)
    {
        var customer = req.ToEntity();
        var creation = Event.Trigger(customer, EventType.Creation);

        await Context.SaveEventAsync(customer, creation, ct);
        await SendAsync(
            RFac.WithSuccess(
                EventResultTrigger.Trigger(creation)), (int)HttpStatusCode.Created, ct);
    }
}
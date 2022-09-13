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
    private readonly DefaultContext _context;

    public CreateCustomerEndpoint(DefaultContext context) => _context = context;

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

        await _context.SaveEventAsync(customer, creation, ct);
        await SendAsync(
            RFac.WithSuccess(
                EventResult<Customer>.Trigger(creation)), (int)HttpStatusCode.Created, ct);
    }
}
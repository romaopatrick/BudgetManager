using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODOrders.Infra;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Create;

public sealed class CreateOrderEndpoint : BaseEndpoint<CreateOrderCommand, EventResult<Order>>
{
    private readonly DefaultContext _context;

    public CreateOrderEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Order>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderCommand req, CancellationToken ct)
    {
        var order = req.ToEntity();
        var creation = Event.Trigger(order, EventType.Creation);

        await _context.SaveEventAsync(order, creation, ct);
        await SendAsync(
            RFac.WithSuccess(EventResult<Order>.Trigger(creation)), (int)HttpStatusCode.Created, ct);
    }
    
}
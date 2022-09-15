using System.Net;
using GODCommon.Results;
using MassTransit;
using IResult = GODCommon.Results.IResult;

namespace GODConductor.Endpoints.Order.Create;

public sealed class CreateOrderEndpoint : PublishEndpoint<CreateOrderCommand>
{
    private readonly IPublishEndpoint _publish;
    public CreateOrderEndpoint(IPublishEndpoint publish) => _publish = publish;

    public override void Configure()
    {
        Post("orders/creations");
        Description(b => b.Produces<IResult>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderCommand req, CancellationToken ct)
    {
        await _publish.Publish(req.Correlate(), ct);
        await SendAsync(RFac.WithSuccess(), cancellation: ct);
    }
}
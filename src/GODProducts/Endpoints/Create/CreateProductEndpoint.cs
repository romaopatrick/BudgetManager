using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODProducts.Infra;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Create;

public sealed class CreateProductEndpoint : BaseEndpoint<CreateProductCommand, EventResult<Product>>
{
    public CreateProductEndpoint(DefaultContext context) : base(context) {}

    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Product>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductCommand req, CancellationToken ct)
    {
        var product = req.ToEntity();
        var creation = Event.Trigger(product, EventType.Creation);

        await Context.SaveEventAsync(product, creation, ct);
        await SendAsync(RFac.WithSuccess(EventResultTrigger.Trigger(creation)), (int)HttpStatusCode.Created, ct);
    }
}
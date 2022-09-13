using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Update;

public sealed class UpdateProductEndpoint : BaseEndpoint<UpdateProductCommand, EventResult<Product>, DefaultContext>
{
    public UpdateProductEndpoint(DefaultContext context) : base(context)
    { }

    public override void Configure()
    {
        Put("updates/{productId}");
        Description(c => c.Produces<IResult<EventResult<Product>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductCommand req, CancellationToken ct)
    {
        var product = await Context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);
        if (product is null)
            await SendAsync(
                RFac.WithError<EventResult<Product>>(ProductNotifications.ProductNotFound),
                (int)HttpStatusCode.NotFound, ct);

        else
        {
            req.UpdateEntity(product);
            var update = Event.Trigger(product, EventType.Update);

            await Context.SaveEventAsync(product, update, ct);

            await SendAsync(
                RFac.WithSuccess(EventResultTrigger.Trigger(update)), cancellation: ct);
        }
    }
}
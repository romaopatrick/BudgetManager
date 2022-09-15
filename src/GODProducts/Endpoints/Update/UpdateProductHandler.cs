using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODProducts.Endpoints.Update;

public class UpdateProductHandler : BaseHandler<UpdateProductCommand, EventResult<Product>>
{
    public UpdateProductHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Product>>> Handle(UpdateProductCommand req, CancellationToken ct)
    {
        var product = await Context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);
        if (product is null) return RFac.WithError<EventResult<Product>>(
            ProductNotifications.ProductNotFound, HttpStatusCode.NotFound);

        req.UpdateEntity(product);
        var update = Event.Trigger(product, EventType.Update);

        await Context.SaveEventAsync(product, update, ct);

        return RFac.WithSuccess(EventResultTrigger.Trigger(update));
    }
}
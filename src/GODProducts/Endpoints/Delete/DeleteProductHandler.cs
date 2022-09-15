using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODProducts.Endpoints.Delete;

public class DeleteProductHandler : BaseHandler<DeleteProductCommand, EventResult<Product>>
{
    public DeleteProductHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Product>>> Handle(DeleteProductCommand req, CancellationToken ct)
    {
        var product = await Context.Products.FirstOrDefaultAsync(x => x.Id == req.ProductId, ct);
        if (product is null) return  RFac.WithError<EventResult<Product>>(
            ProductNotifications.ProductNotFound, HttpStatusCode.NotFound);
        
        Context.Products.Remove(product);
        var deletion = Event.Trigger(product, EventType.Deletion);
        await Context.SaveEventAsync(deletion, ct);

        return RFac.WithSuccess(EventResultTrigger.Trigger(deletion));
    }
}
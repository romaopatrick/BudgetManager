using System.Net;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Detail;

public class DetailProductEndpoint : BaseEndpoint<DetailProductCommand, Product>
{
    public DetailProductEndpoint(DefaultContext context) : base(context)
    {
    }

    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Product>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DetailProductCommand req, CancellationToken ct)
    {
        var product = await Context.Products.FirstOrDefaultAsync(x => x.SnapshotNumber == req.SnapshotNumber, ct);
        if (product is null)
            await SendAsync(
                RFac.WithError<Product>(
                    ProductNotifications.ProductNotFound), (int)HttpStatusCode.NotFound, ct);
        else await SendAsync(RFac.WithSuccess(product), cancellation: ct);
    }
}
using System.Net;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using Microsoft.EntityFrameworkCore;

namespace GODProducts.Endpoints.Detail;

public class DetailProductHandler : BaseHandler<DetailProductCommand, Product>
{
    public DetailProductHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Product>> Handle(DetailProductCommand req, CancellationToken ct)
    {
        var product = await Context.Products.FirstOrDefaultAsync(x => x.SnapshotNumber == req.SnapshotNumber, ct);
        return product is null 
            ? RFac.WithError<Product>(ProductNotifications.ProductNotFound, HttpStatusCode.NotFound)
            : RFac.WithSuccess(product);
    }
}
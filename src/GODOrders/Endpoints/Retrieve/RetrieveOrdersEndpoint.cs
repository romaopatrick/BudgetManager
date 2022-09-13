using System.Net;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODOrders.Infra;
using Microsoft.AspNetCore.Mvc;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Retrieve;

public sealed class RetrieveOrdersEndpoint : BaseEndpoint<RetrieveOrdersCommand, Paged<Order>>
{
    public RetrieveOrdersEndpoint(DefaultContext context) : base(context) {}

    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Order>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] RetrieveOrdersCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip!.Value, req.Range!.Value, req.KeyToOrder!, req.Desc!.Value, ct);

        await SendAsync(
            RFac.WithSuccess(result), cancellation: ct);
    }

    private IQueryable<Order> Query(RetrieveOrdersCommand req)
    {
        var query = Context.Orders.AsQueryable();

        if (req.Status.HasValue)
            query = query.Where(x => x.Status == req.Status);
        
        if (req.CustomerNumber.HasValue)
            query = query.Where(x => x.CustomerNumber == req.CustomerNumber);
        
        if (req.ProductNumber.HasValue)
            query = query.Where(x => x.ProductNumber == req.ProductNumber);
        
        return query;
    }
}
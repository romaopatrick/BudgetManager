using System.Net;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODProducts.Infra;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace GODProducts.Endpoints.Retrieve;

public sealed class RetrieveProductsEndpoint : BaseEndpoint<RetrieveProductsCommand, Paged<Product>, DefaultContext>
{
    public RetrieveProductsEndpoint(DefaultContext context) : base(context)
    { }

    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Product>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(RetrieveProductsCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip, req.Range, req.KeyToOrder, req.Desc, ct);

        await SendAsync(RFac.WithSuccess(result), cancellation: ct);
    }

    private IQueryable<Product> Query(RetrieveProductsCommand req)
    {
        var query = Context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(req.Brand))
            query = query.Where(x => x.Brand.Contains(req.Brand));
        if (!string.IsNullOrWhiteSpace(req.Name))
            query = query.Where(x => x.Name.Contains(req.Name));
        if (!string.IsNullOrWhiteSpace(req.SerialNumber))
            query = query.Where(x => !string.IsNullOrWhiteSpace(x.SerialNumber) 
                                     && x.SerialNumber.Contains(req.SerialNumber));
        if (req.Guarantee.HasValue)
            query = query.Where(x => x.Guarantee == req.Guarantee);
        if (req.CustomerNumber.HasValue)
            query = query.Where(x => x.CustomerNumber == req.CustomerNumber);
        if (req.EntryDateMax.HasValue)
            query = query.Where(x => x.EntryDate <= req.EntryDateMax);
        if (req.EntryDateMin.HasValue)
            query = query.Where(x => x.EntryDate >= req.EntryDateMin);
        if (req.ExitDateMax.HasValue)
            query = query.Where(x => x.ExitDate.HasValue && x.ExitDate <= req.ExitDateMax);
        if (req.ExitDateMin.HasValue)
            query = query.Where(x => x.ExitDate.HasValue && x.ExitDate >= req.ExitDateMin);

        return query;
    }
}
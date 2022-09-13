using System.Net;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODCustomers.Infra;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Retrieve;

public class RetrieveCustomersEndpoint : BaseEndpoint<RetrieveCustomersCommand, Paged<Customer>>
{
    private readonly DefaultContext _context;

    public RetrieveCustomersEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Customer>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(RetrieveCustomersCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip, req.Range, req.KeyToOrder, req.Desc, ct);
        await SendAsync(RFac.WithSuccess(result), cancellation: ct);
    }

    private IQueryable<Customer> Query(RetrieveCustomersCommand req)
    {
        var query = _context.Customers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(req.Document))
            query = query.Where(x => x.Document.Contains(req.Document));

        if (!string.IsNullOrEmpty(req.FullName))
            query = query.Where(x => x.FullName.Contains(req.FullName.ToUpper()));

        if (req.Status.HasValue)
            query = query.Where(x => x.Status == req.Status);
        
        return query;
    }
}
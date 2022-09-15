using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODCustomers.Infra;

namespace GODCustomers.Endpoints.Retrieve;

public class RetrieveCustomersHandler : BaseHandler<RetrieveCustomersCommand, Paged<Customer>>
{
    public RetrieveCustomersHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Paged<Customer>>> Handle(RetrieveCustomersCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip, req.Range, req.KeyToOrder, req.Desc, ct);
        return RFac.WithSuccess(result);
    }
    private IQueryable<Customer> Query(RetrieveCustomersCommand req)
    {
        var query = Context.Customers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(req.Document))
            query = query.Where(x => x.Document.Contains(req.Document));

        if (!string.IsNullOrEmpty(req.FullName))
            query = query.Where(x => x.FullName.Contains(req.FullName.ToUpper()));

        if (req.Status.HasValue)
            query = query.Where(x => x.Status == req.Status);
        
        return query;
    }
}
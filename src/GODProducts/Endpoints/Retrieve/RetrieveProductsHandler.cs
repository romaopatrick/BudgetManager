using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODProducts.Infra;

namespace GODProducts.Endpoints.Retrieve;

public class RetrieveProductsHandler : BaseHandler<RetrieveProductsCommand, Paged<Product>>
{
    public RetrieveProductsHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Paged<Product>>> Handle(RetrieveProductsCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip, req.Range, req.KeyToOrder, req.Desc, ct);

        return RFac.WithSuccess(result);
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
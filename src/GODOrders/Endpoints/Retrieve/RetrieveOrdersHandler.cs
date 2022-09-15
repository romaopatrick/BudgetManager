using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODOrders.Infra;
using MediatR;

namespace GODOrders.Endpoints.Retrieve;

public class RetrieveOrdersHandler : BaseHandler<RetrieveOrdersCommand, Paged<Order>>
{
    public RetrieveOrdersHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Paged<Order>>> Handle(RetrieveOrdersCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip!.Value, req.Range!.Value, req.KeyToOrder!, req.Desc!.Value, ct);
        return RFac.WithSuccess(result);
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
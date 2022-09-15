using GODBudgets.Infra;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;

namespace GODBudgets.Endpoints.Retrieve;

public class RetrieveBudgetsHandler : BaseHandler<RetrieveBudgetsCommand, Paged<Budget>>
{
    public RetrieveBudgetsHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Paged<Budget>>> Handle(RetrieveBudgetsCommand req, CancellationToken ct)
    {
        var result = await Query(req).Page(req.Skip, req.Range, req.KeyToOrder, req.Desc, ct);
        return RFac.WithSuccess(result);
    }
    
    private IQueryable<Budget> Query(RetrieveBudgetsCommand req)
    {
        var query = Context.Budgets.AsQueryable();
        if (req.Status.HasValue)
            query = query.Where(b => b.Status == req.Status);

        if (req.OrderNumber.HasValue)
            query = query.Where(b => b.OrderNumber == req.OrderNumber);

        if (req.ProposedValueMax.HasValue)
            query = query.Where(b => b.ProposedValue <= req.ProposedValueMax);

        if (req.ProposedValueMin.HasValue)
            query = query.Where(b => b.ProposedValue >= req.ProposedValueMin);

        if (req.WorkingDaysToCompleteMax.HasValue)
            query = query.Where(b => b.WorkingDaysToComplete <= req.WorkingDaysToCompleteMax);
        
        if (req.WorkingDaysToCompleteMin.HasValue)
            query = query.Where(b => b.WorkingDaysToComplete >= req.WorkingDaysToCompleteMin);
        
        return query;
    }
}
using System.Net;
using GODBudgets.Infra;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;

namespace GODBudgets.Endpoints.Detail;

public class DetailBudgetHandler : BaseHandler<DetailBudgetCommand, Budget>
{
    public DetailBudgetHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<Budget>> Handle(DetailBudgetCommand req, CancellationToken ct)
    {
        var budget = await Context.Budgets.FirstOrDefaultAsync(b => b.SnapshotNumber == req.SnapshotNumber, ct);
        if (budget is null) return RFac.WithError<Budget>(
            BudgetNotifications.BudgetNotFound, HttpStatusCode.NotFound);
        
        return RFac.WithSuccess(budget);
    }
}
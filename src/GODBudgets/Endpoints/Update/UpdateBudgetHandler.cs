using System.Net;
using GODBudgets.Infra;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;

namespace GODBudgets.Endpoints.Update;

public class UpdateBudgetHandler : BaseHandler<UpdateBudgetCommand, EventResult<Budget>>
{
    public UpdateBudgetHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Budget>>> Handle(UpdateBudgetCommand req, CancellationToken ct)
    {
        var budget = await Context.Budgets.FirstOrDefaultAsync(x => x.Id == req.BudgetId, ct);
        if (budget is null) return RFac.WithError<EventResult<Budget>>(
            BudgetNotifications.BudgetNotFound, HttpStatusCode.NotFound);
        
        req.UpdateEntity(budget);
        var update = Event.Trigger(budget, EventType.Update);

        await Context.SaveEventAsync(budget, update, ct);
        return RFac.WithSuccess(EventResultTrigger.Trigger(update));
    }
}
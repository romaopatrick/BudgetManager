using System.Net;
using GODBudgets.Infra;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;

namespace GODBudgets.Endpoints.Delete;

public class DeleteBudgetHandler : BaseHandler<DeleteBudgetCommand, EventResult<Budget>>
{
    public DeleteBudgetHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Budget>>> Handle(DeleteBudgetCommand req, CancellationToken ct)
    {
        var budgetToDisable = await Context.Budgets.FirstOrDefaultAsync(x => x.Id == req.BudgetId, ct);
        if (budgetToDisable is null) return RFac.WithError<EventResult<Budget>>(
            BudgetNotifications.BudgetNotFound, HttpStatusCode.NotFound);
        
        Context.Budgets.Remove(budgetToDisable);
        var deletion = Event.Trigger(budgetToDisable, EventType.Deletion);
        await Context.SaveEventAsync(deletion, ct);

        return RFac.WithSuccess(EventResultTrigger.Trigger(deletion));
    }
}
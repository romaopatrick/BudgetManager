using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Delete;

public sealed class DeleteBudgetEndpoint : BaseEndpoint<DeleteBudgetCommand, EventResult<Budget>>
{
    public DeleteBudgetEndpoint(DefaultContext context) : base(context) {}

    public override void Configure()
    {
        Delete("deletions/{budgetId}");
        Description(c => c.Produces<IResult<EventResult<Budget>>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteBudgetCommand req, CancellationToken ct)
    {
        var budgetToDisable = await Context.Budgets.FirstOrDefaultAsync(x => x.Id == req.BudgetId, ct);
        if (budgetToDisable is null)
            await SendAsync(
                RFac.WithError<EventResult<Budget>>(BudgetNotifications.BudgetNotFound),
                (int)HttpStatusCode.NotFound, ct);
        else
        {
            Context.Budgets.Remove(budgetToDisable);

            var deletion = Event.Trigger(budgetToDisable, EventType.Deletion);
            await Context.SaveEventAsync(deletion, ct);

            await SendAsync(RFac.WithSuccess(EventResultTrigger.Trigger(deletion)), (int)HttpStatusCode.OK, ct);
        }
    }
}
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
    private readonly DefaultContext _context;

    public DeleteBudgetEndpoint(DefaultContext context) => _context = context;

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
        var budgetToDisable = await _context.Budgets.FirstOrDefaultAsync(x => x.Id == req.BudgetId, ct);
        if (budgetToDisable is null)
            await SendAsync(
                RFac.WithError<EventResult<Budget>>(BudgetNotifications.BudgetNotFound),
                (int)HttpStatusCode.NotFound, ct);
        else
        {
            _context.Budgets.Remove(budgetToDisable);

            var deletion = Event.Trigger(budgetToDisable!, EventType.Deletion);
            await _context.SaveEventAsync(deletion, ct);

            await SendAsync(RFac.WithSuccess(EventResult<Budget>.Trigger(deletion)), (int)HttpStatusCode.OK, ct);
        }
    }
}
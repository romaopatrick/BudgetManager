using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Update;

public sealed class UpdateBudgetEndpoint : BaseEndpoint<UpdateBudgetCommand, EventResult<Budget>>
{
    private readonly DefaultContext _context;

    public UpdateBudgetEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Put("updates/{budgetId}");
        Description(c => c.Produces<IResult<EventResult<Budget>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBudgetCommand req, CancellationToken ct)
    {
        var budget = await _context.Budgets.FirstOrDefaultAsync(x => x.Id == req.BudgetId, ct);
        if (budget is null) await BudgetNotFoundFail(ct);
        
        else await Success(req, budget, ct);
    }

    private Task BudgetNotFoundFail(CancellationToken ct)
        => SendAsync(RFac.WithError<EventResult<Budget>>(
            BudgetNotifications.BudgetNotFound), (int)HttpStatusCode.BadRequest, ct);
    private async Task Success(UpdateBudgetCommand req, Budget budget, CancellationToken ct)
    {
        req.UpdateEntity(budget);
        var update = Event.Trigger(budget, EventType.Update);

        await _context.SaveEventAsync(budget, update, ct);
        await SendAsync(RFac.WithSuccess(EventResult<Budget>.Trigger(update)), (int)HttpStatusCode.OK, ct);
    }
}
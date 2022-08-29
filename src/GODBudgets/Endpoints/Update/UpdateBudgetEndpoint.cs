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

public class UpdateBudgetEndpoint : BaseEndpoint<UpdateBudgetCommand, EventResult<Budget>>
{
    private readonly DefaultContext _context;

    public UpdateBudgetEndpoint(DefaultContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("updates/{BudgetId}");
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
        => SendAsync(ResultFactory.WithError<EventResult<Budget>>(
            BudgetNotifications.BudgetNotFound), (int)HttpStatusCode.NotFound, ct);
    private async Task Success(UpdateBudgetCommand req, Budget budget, CancellationToken ct)
    {
        req.UpdateEntity(budget);
        await Save(budget);

        var update = Event.Trigger(budget, EventType.Update);
        await Save(update);

        await SendAsync(ResultFactory.WithSuccess(EventResult<Budget>.Trigger(update)), (int)HttpStatusCode.OK, ct);
    }
    private async Task Save(Budget b)
    {
        _context.Update(b);
        await _context.SaveChangesAsync();
    }

    private async Task Save(Event e)
    {
        await _context.AddAsync(e);
        await _context.SaveChangesAsync();
    }

}
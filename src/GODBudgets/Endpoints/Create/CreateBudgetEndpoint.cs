using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Create;

public class CreateBudgetEndpoint : BaseEndpoint<CreateBudgetCommand, EventResult<Budget>>
{
    private readonly DefaultContext _context;

    public CreateBudgetEndpoint(DefaultContext context)
    {
        _context = context;
    }
    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Budget>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBudgetCommand req, CancellationToken ct)
    {
        if (await BudgetInProgress(req, ct)) await BudgetInProgressFail(ct);
        
        else await Success(req, ct);
    }

    private Task BudgetInProgressFail(CancellationToken ct)
        => SendAsync(
            ResultFactory.WithError<EventResult<Budget>>(BudgetNotifications.AnotherBudgetInProgress),
            (int)HttpStatusCode.BadRequest, ct);
    private Task<bool> BudgetInProgress(CreateBudgetCommand req, CancellationToken ct) =>
        _context.Budgets.AnyAsync(x =>
            x.OrderNumber == req.OrderNumber &&
            (x.Status != BudgetStatus.Pending
             || x.Status != BudgetStatus.Canceled), ct);



    private async Task Success(CreateBudgetCommand req, CancellationToken ct)
    {
        var budget = req.ToEntity();
        await Save(budget);
        
        var creation = Event.Trigger(budget, EventType.Creation);
        await Save(creation);
        
        await SendAsync(ResultFactory.WithSuccess(EventResult<Budget>.Trigger(creation)), (int)HttpStatusCode.Created, ct);
    }
    private async Task Save(Budget b)
    {
        await _context.AddAsync(b);
        await _context.SaveChangesAsync();
    }
    private async Task Save(Event bc)
    {
        await _context.AddAsync(bc);
        await _context.SaveChangesAsync();
    }


}
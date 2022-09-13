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

public sealed class CreateBudgetEndpoint : BaseEndpoint<CreateBudgetCommand, EventResult<Budget>>
{
    public CreateBudgetEndpoint(DefaultContext context) : base(context) {}
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
            RFac.WithError<EventResult<Budget>>(BudgetNotifications.AnotherBudgetInProgress),
            (int)HttpStatusCode.BadRequest, ct);
    private Task<bool> BudgetInProgress(CreateBudgetCommand req, CancellationToken ct) =>
        Context.Budgets.AnyAsync(x =>
            x.OrderNumber == req.OrderNumber &&
            (x.Status != BudgetStatus.Pending
             || x.Status != BudgetStatus.Canceled), ct);



    private async Task Success(CreateBudgetCommand req, CancellationToken ct)
    {
        var budget = req.ToEntity();
        var creation = Event.Trigger(budget, EventType.Creation);
        
        await Context.SaveEventAsync(budget, creation, ct);
        await SendAsync(
            RFac.WithSuccess(EventResultTrigger.Trigger(creation)), (int)HttpStatusCode.Created, ct);
    }

}
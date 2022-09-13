using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Detail;

public sealed class DetailBudgetEndpoint : BaseEndpoint<DetailBudgetCommand, Budget>
{
    private readonly DefaultContext _context;

    public DetailBudgetEndpoint(DefaultContext context) => _context = context;

    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Budget>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(DetailBudgetCommand req, CancellationToken ct)
    {
        var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.SnapshotNumber == req.SnapshotNumber, ct);
        if (budget is null)
            await SendAsync(RFac.WithError<Budget>(BudgetNotifications.BudgetNotFound),
                (int)HttpStatusCode.NotFound, ct);
        
        else await SendAsync(RFac.WithSuccess(budget), cancellation: ct);
    }
}
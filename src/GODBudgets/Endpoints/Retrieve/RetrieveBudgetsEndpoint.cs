using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using Microsoft.AspNetCore.Mvc;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Retrieve;

public sealed class RetrieveBudgetsEndpoint : BaseEndpoint<RetrieveBudgetsCommand, Paged<Budget>>
{
    public RetrieveBudgetsEndpoint(DefaultContext context) : base(context) {}
    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Budget>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync([FromQuery] RetrieveBudgetsCommand req, CancellationToken ct)
    {
        var result = await Query(req)
            .Page(req.Skip!.Value, req.Range!.Value, req.KeyToOrder!, req.Desc!.Value, ct);
        await SendAsync(RFac.WithSuccess(result), cancellation: ct);
    }

    private IQueryable<Budget> Query(RetrieveBudgetsCommand req)
    {
        var query = Context.Budgets.AsQueryable();
        if (req.Status.HasValue)
            query = query.Where(b => b.Status == req.Status);

        if (req.OrderNumber.HasValue)
            query = query.Where(b => b.OrderNumber == req.OrderNumber);

        if (req.ProposedValueMax.HasValue)
            query = query.Where(b => b.ProposedValue <= req.ProposedValueMax);

        if (req.ProposedValueMin.HasValue)
            query = query.Where(b => b.ProposedValue >= req.ProposedValueMin);

        if (req.WorkingDaysToCompleteMax.HasValue)
            query = query.Where(b => b.WorkingDaysToComplete <= req.WorkingDaysToCompleteMax);
        
        if (req.WorkingDaysToCompleteMin.HasValue)
            query = query.Where(b => b.WorkingDaysToComplete >= req.WorkingDaysToCompleteMin);
        
        return query;
    }
}
using System.Net;
using GODCommon.Results;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Create;

public class CreateBudgetEndpoint : Endpoint<CreateBudgetCommand, IResult<BudgetCreatedEvent>, CreationMapper>
{
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("creation");
        Description(b => b
            .Produces<IResult<BudgetCreatedEvent>>((int)HttpStatusCode.Created, "application/json")
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBudgetCommand req, CancellationToken ct)
    {
        var entity = await Map.ToEntityAsync(req);
        var result = Map.FromEntity(entity);
        await SendAsync(result, result.StatusCode, ct);
    }
}
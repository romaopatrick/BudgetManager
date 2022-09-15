using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Delete;

public sealed class DeleteBudgetEndpoint : BaseEndpoint<DeleteBudgetCommand, EventResult<Budget>>
{
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
        
    }
    public DeleteBudgetEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
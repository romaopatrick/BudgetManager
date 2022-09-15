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

namespace GODBudgets.Endpoints.Update;

public sealed class UpdateBudgetEndpoint : BaseEndpoint<UpdateBudgetCommand, EventResult<Budget>>
{
    public override void Configure()
    {
        Put("updates/{budgetId}");
        Description(c => c.Produces<IResult<EventResult<Budget>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public UpdateBudgetEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
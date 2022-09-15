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

namespace GODBudgets.Endpoints.Create;

public sealed class CreateBudgetEndpoint : BaseEndpoint<CreateBudgetCommand, EventResult<Budget>>
{
    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Budget>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }
    public CreateBudgetEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
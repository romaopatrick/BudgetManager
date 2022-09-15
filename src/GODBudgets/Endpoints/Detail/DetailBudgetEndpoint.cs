using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Notifications;
using GODCommon.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Detail;

public sealed class DetailBudgetEndpoint : BaseEndpoint<DetailBudgetCommand, Budget>
{
    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Budget>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public DetailBudgetEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
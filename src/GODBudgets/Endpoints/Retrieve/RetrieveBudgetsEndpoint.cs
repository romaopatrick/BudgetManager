using System.Net;
using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using IResult = GODCommon.Results.IResult;

namespace GODBudgets.Endpoints.Retrieve;

public sealed class RetrieveBudgetsEndpoint : BaseEndpoint<RetrieveBudgetsCommand, Paged<Budget>>
{
    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Budget>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public RetrieveBudgetsEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
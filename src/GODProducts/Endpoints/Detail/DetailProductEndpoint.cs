using System.Net;
using GODCommon.Endpoints;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Detail;

public class DetailProductEndpoint : BaseEndpoint<DetailProductCommand, Product>
{
    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Product>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }
    public DetailProductEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Update;

public sealed class UpdateProductEndpoint : BaseEndpoint<UpdateProductCommand, EventResult<Product>>
{
    public override void Configure()
    {
        Put("updates/{productId}");
        Description(c => c.Produces<IResult<EventResult<Product>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public UpdateProductEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
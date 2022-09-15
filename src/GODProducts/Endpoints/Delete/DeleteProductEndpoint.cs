using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODProducts.Infra;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Delete;

public class DeleteProductEndpoint : BaseEndpoint<DeleteProductCommand, EventResult<Product>>
{

    public override void Configure()
    {
        Delete("deletions/{productId}");
        Description(c => c.Produces<IResult<EventResult<Product>>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }
    public DeleteProductEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
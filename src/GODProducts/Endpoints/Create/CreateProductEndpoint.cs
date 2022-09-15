using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODProducts.Infra;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODProducts.Endpoints.Create;

public sealed class CreateProductEndpoint : BaseEndpoint<CreateProductCommand, EventResult<Product>>
{
    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Product>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public CreateProductEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
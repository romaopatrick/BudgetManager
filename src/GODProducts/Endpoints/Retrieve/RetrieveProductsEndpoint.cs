using System.Net;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODProducts.Infra;
using MediatR;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace GODProducts.Endpoints.Retrieve;

public sealed class RetrieveProductsEndpoint : BaseEndpoint<RetrieveProductsCommand, Paged<Product>>
{
    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Product>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }
    public RetrieveProductsEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
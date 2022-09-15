using System.Net;
using GODCommon.Endpoints;
using GODCommon.Results;
using GODCommon.Results.Paging;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Retrieve;

public sealed class RetrieveOrdersEndpoint : BaseEndpoint<RetrieveOrdersCommand, Paged<Order>>
{
    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Order>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }


    public RetrieveOrdersEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
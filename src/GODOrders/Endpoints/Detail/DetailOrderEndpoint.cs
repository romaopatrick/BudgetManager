using System.Net;
using GODCommon.Endpoints;
using GODCommon.Results;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODOrders.Endpoints.Detail;

public sealed class DetailOrderEndpoint : BaseEndpoint<DetailOrderCommand, Order>
{
    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Order>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public DetailOrderEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
using System.Net;
using GODCommon.Endpoints;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Detail;

public sealed class DetailCustomerEndpoint : BaseEndpoint<DetailCustomerCommand, Customer>
{
    public override void Configure()
    {
        Get("details/{snapshotNumber}");
        Description(c => c.Produces<IResult<Customer>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }
    public DetailCustomerEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
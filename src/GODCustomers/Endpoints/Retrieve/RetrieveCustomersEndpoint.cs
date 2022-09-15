using System.Net;
using GODCommon.Endpoints;
using GODCommon.Extensions;
using GODCommon.Results;
using GODCommon.Results.Paging;
using GODCustomers.Infra;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Retrieve;

public class RetrieveCustomersEndpoint : BaseEndpoint<RetrieveCustomersCommand, Paged<Customer>>
{
    public override void Configure()
    {
        Get("paging");
        Description(b => b
            .Produces<IResult<Paged<Customer>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public RetrieveCustomersEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODCustomers.Infra;
using MediatR;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Create;

public sealed class CreateCustomerEndpoint : BaseEndpoint<CreateCustomerCommand, EventResult<Customer>>
{
    public override void Configure()
    {
        Post("creations");
        Description(b => b
            .Produces<IResult<EventResult<Customer>>>((int)HttpStatusCode.Created)
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public CreateCustomerEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
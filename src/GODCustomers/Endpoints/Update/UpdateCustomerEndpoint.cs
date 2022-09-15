using System.Net;
using GODCommon.Endpoints;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Notifications;
using GODCommon.Results;
using GODCustomers.Infra;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IResult = GODCommon.Results.IResult;

namespace GODCustomers.Endpoints.Update;

public sealed class UpdateCustomerEndpoint : BaseEndpoint<UpdateCustomerCommand, EventResult<Customer>>
{
    public override void Configure()
    {
        Put("updates/{customerId}");
        Description(c => c.Produces<IResult<EventResult<Customer>>>()
            .Produces<IResult>((int)HttpStatusCode.BadRequest)
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCustomerCommand req, CancellationToken ct)
    {

    }

    public UpdateCustomerEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
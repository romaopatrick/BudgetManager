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

namespace GODCustomers.Endpoints.Delete;

public class DeleteCustomerEndpoint : BaseEndpoint<DeleteCustomerCommand, EventResult<Customer>>
{
    public override void Configure()
    {
        Delete("deletions/{customerId}");
        Description(c => c.Produces<IResult<EventResult<Customer>>>()
            .Produces<IResult>((int)HttpStatusCode.NotFound)
            .Produces<IResult>((int)HttpStatusCode.InternalServerError));
        AllowAnonymous();
    }

    public DeleteCustomerEndpoint(IMediator mediator) : base(mediator)
    {
    }
}
using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODCustomers.Endpoints.Delete;

public sealed class DeleteCustomerCommand : IRequest<IResult<EventResult<Customer>>>
{
    public Guid CustomerId { get; init; }
}
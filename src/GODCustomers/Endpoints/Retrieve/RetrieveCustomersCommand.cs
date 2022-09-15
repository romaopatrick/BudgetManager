using GODCommon.Enums;
using GODCommon.Results;
using GODCommon.Results.Paging;
using MediatR;

namespace GODCustomers.Endpoints.Retrieve;

public sealed class RetrieveCustomersCommand : Paged, IRequest<IResult<Paged<Customer>>>
{
    public string? Document { get; init; }
    public string? FullName { get; init; }
    public CustomerStatus? Status { get; init; }
}
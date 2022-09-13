using GODCommon.Enums;
using GODCommon.Results.Paging;

namespace GODCustomers.Endpoints.Retrieve;

public sealed class RetrieveCustomersCommand : Paged
{
    public string? Document { get; init; }
    public string? FullName { get; init; }
    public CustomerStatus? Status { get; init; }
}
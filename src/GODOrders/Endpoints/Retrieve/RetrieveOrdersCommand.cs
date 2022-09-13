using GODCommon.Enums;
using GODCommon.Results.Paging;

namespace GODOrders.Endpoints.Retrieve;

public sealed class RetrieveOrdersCommand : Paged
{
    public long? CustomerNumber { get; init; }
    public long? ProductNumber { get; init; }
    public OrderStatus? Status { get; init; }
}
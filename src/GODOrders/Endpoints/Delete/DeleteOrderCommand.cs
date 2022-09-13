namespace GODOrders.Endpoints.Delete;

public sealed class DeleteOrderCommand
{
    public Guid OrderId { get; init; }
}
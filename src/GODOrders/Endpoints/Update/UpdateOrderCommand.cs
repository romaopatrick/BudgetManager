namespace GODOrders.Endpoints.Update;

public sealed class UpdateOrderCommand 
{
    public Guid OrderId { get; init; }
    public string? Details { get; init; }

    public void UpdateEntity(Order order)
    {
        if (Details is not null)
            order.Details = Details;
    }
}
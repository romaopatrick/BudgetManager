using GODOrders.Consumers.Create;

namespace GODConductor.Endpoints.Order.Create;

public sealed class CreateOrderCommand : GODOrders.Endpoints.Create.CreateOrderCommand
{
    public CorrelatedCreateOrderCommand Correlate() => new()
    {
        Details = Details,
        CorrelationId = Guid.NewGuid(),
        CustomerNumber = CustomerNumber,
        ProductNumber = ProductNumber
    };
}
using GODCommon.Consumers;
using GODOrders.Endpoints.Create;

namespace GODOrders.Consumers.Create;

public sealed class CorrelatedCreateOrderCommand : CreateOrderCommand, ICorrelated<CreateOrderCommand>
{
    public CreateOrderCommand UnCorrelate() => new ()
    {
        Details = Details,
        CustomerNumber = CustomerNumber,
        ProductNumber = ProductNumber
    };

    public Guid CorrelationId { get; init; }
}
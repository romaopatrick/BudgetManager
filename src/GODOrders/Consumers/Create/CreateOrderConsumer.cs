using GODCommon.Consumers;
using GODCommon.Events;
using MediatR;

namespace GODOrders.Consumers.Create;

public sealed class CreateOrderConsumer : BaseConsumer<CorrelatedCreateOrderCommand, EventResult<Order>>
{
    public CreateOrderConsumer(IMediator mediator, ILogger<CreateOrderConsumer> logger) : base(mediator, logger)
    {
    }
}
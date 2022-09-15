using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODOrders.Infra;

namespace GODOrders.Endpoints.Create;

public sealed class CreateOrderHandler : BaseHandler<CreateOrderCommand, EventResult<Order>>
{
    public CreateOrderHandler(DefaultContext context) : base(context)
    {
    }
    public override async Task<IResult<EventResult<Order>>> Handle(CreateOrderCommand req, CancellationToken ct)
    {
        var order = req.ToEntity();
        var creation = Event.Trigger(order, EventType.Creation);

        await Context.SaveEventAsync(order, creation, ct);

        return RFac.WithSuccess(EventResultTrigger.Trigger(creation));
    }

    
}
using System.Net;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using GODProducts.Infra;

namespace GODProducts.Endpoints.Create;

public class CreateProductHandler : BaseHandler<CreateProductCommand, EventResult<Product>>
{
    public CreateProductHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Product>>> Handle(CreateProductCommand req, CancellationToken ct)
    {
        var product = req.ToEntity();
        var creation = Event.Trigger(product, EventType.Creation);

        await Context.SaveEventAsync(product, creation, ct);
        
        return RFac.WithSuccess(EventResultTrigger.Trigger(creation), HttpStatusCode.Created);
    }
}
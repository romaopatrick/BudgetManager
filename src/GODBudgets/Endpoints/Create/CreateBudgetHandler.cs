using System.Net;
using GODBudgets.Infra;
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;

namespace GODBudgets.Endpoints.Create;

public class CreateBudgetHandler : BaseHandler<CreateBudgetCommand, EventResult<Budget>>
{
    public CreateBudgetHandler(DefaultContext context) : base(context)
    {
    }

    public override async Task<IResult<EventResult<Budget>>> Handle(CreateBudgetCommand req, CancellationToken ct)
    {
        var budget = req.ToEntity();
        var creation = Event.Trigger(budget, EventType.Creation);
        
        await Context.SaveEventAsync(budget, creation, ct);
        return RFac.WithSuccess(EventResultTrigger.Trigger(creation), HttpStatusCode.Created);
    }
}
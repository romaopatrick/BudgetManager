using GODBudgets.Infra;
using GODCommon.Enums;
using GODCommon.Notifications;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;

namespace GODBudgets.Endpoints.Create;

public class CreationMapper : Mapper<CreateBudgetCommand, IResult<BudgetCreatedEvent>, IResult<Event>>
{
    private readonly DefaultContext _context;

    public CreationMapper() => _context = TryResolve<DefaultContext>() ?? throw new ArgumentNullException(nameof(_context));

    public override IResult<BudgetCreatedEvent> FromEntity(IResult<Event> e) => !e.Success 
        ? ResultFactory.WithError<BudgetCreatedEvent>(e.Notifications, e.StatusCode) 
        : ResultFactory.WithSuccess(BudgetCreatedEvent.Trigger(e.Data));
    public override async Task<IResult<Event>> ToEntityAsync(CreateBudgetCommand r)
    {
        if (await _context.Budgets.AnyAsync(x =>
                x.OrderNumber == r.OrderNumber && x.Status != BudgetStatus.Pending))
            return ResultFactory.WithError<Event>(BudgetNotifications.ANOTHER_BUDGET_IN_PROGRESS);

        var budget = r.ToEntity();
        await Save(budget);
        
        var creation = Event.Trigger(budget, EventType.Creation);
        await Save(creation);
        
        return ResultFactory.WithSuccess(creation);
    }

    private async Task Save(Budget b)
    {
        await _context.AddAsync(b);
        await _context.SaveChangesAsync();
    }

    private async Task Save(Event bc)
    {
        await _context.AddAsync(bc);
        await _context.SaveChangesAsync();
    }

}
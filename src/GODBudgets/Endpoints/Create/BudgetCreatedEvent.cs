namespace GODBudgets.Endpoints.Create;

public class BudgetCreatedEvent
{
    public Guid EventId { get; init; }
    public DateTime EventDate { get; init; }
    public Budget Snapshot { get; init; }
    public static BudgetCreatedEvent Trigger(Event bc)
        => new()
        {
            Snapshot = bc.Snapshot,
            EventId = bc.Id,
            EventDate = bc.CreatedAt.Value
        };
}
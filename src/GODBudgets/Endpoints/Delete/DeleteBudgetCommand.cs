namespace GODBudgets.Endpoints.Delete;

public sealed class DeleteBudgetCommand
{
    public Guid BudgetId { get; init; }
}
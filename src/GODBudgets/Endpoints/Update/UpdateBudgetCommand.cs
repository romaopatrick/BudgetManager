using GODCommon.Enums;

namespace GODBudgets.Endpoints.Update;

public sealed class UpdateBudgetCommand
{
    public Guid BudgetId { get; init; }
    public UpdateBudgetStatus? Status { get; init; }
    public decimal? ProposedValue { get; init; }
    public string? Details { get; init; }    
    
    public int? WorkingDaysToComplete { get; set; }
    public bool? SendEmailOnComplete { get; init; }
    private BudgetStatus BudgetStatus => Status switch
    {
        UpdateBudgetStatus.Accepted => BudgetStatus.Accepted,
        UpdateBudgetStatus.Awaiting => BudgetStatus.Awaiting,
        UpdateBudgetStatus.Doing => BudgetStatus.Doing,
        _ => BudgetStatus.Doing
    };

    public void UpdateEntity(Budget previousSnapshot)
    {
        if (Details is not null)
            previousSnapshot.Details = Details;

        if (Status is not null)
            previousSnapshot.Status = BudgetStatus;

        if (ProposedValue is not null)
            previousSnapshot.ProposedValue = ProposedValue;

        if (WorkingDaysToComplete is not null)
            previousSnapshot.WorkingDaysToComplete = WorkingDaysToComplete;

        if (SendEmailOnComplete is not null)
            previousSnapshot.SendEmailOnComplete = (bool)SendEmailOnComplete;
    }
}
public enum UpdateBudgetStatus
{
    Accepted,
    Awaiting,
    Doing
}
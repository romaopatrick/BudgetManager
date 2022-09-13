using GODCommon.Enums;

namespace GODBudgets.Endpoints.Create;

public sealed class CreateBudgetCommand
{
    public long OrderNumber { get; init; }
    public decimal? ProposedValue { get; init; }    
    public int? WorkingDaysToComplete { get; set; }

    public string? Details { get; init; }
    public bool SendEmailOnComplete { get; init; }

    public Budget ToEntity() => new()
    {
        Details = Details,
        Status = BudgetStatus.Pending,
        OrderNumber = OrderNumber,
        WorkingDaysToComplete = WorkingDaysToComplete,
        SendEmailOnComplete = SendEmailOnComplete,
        ProposedValue = ProposedValue
    };
}
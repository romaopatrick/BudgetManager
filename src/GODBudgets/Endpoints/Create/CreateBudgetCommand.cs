using GODCommon.Enums;

namespace GODBudgets.Endpoints.Create;

public class CreateBudgetCommand
{
    public long OrderNumber { get; init; }
    public decimal? ProposedValue { get; init; }
    public DateTime? ExpectedCompletionDate { get; init; }
    public string? Details { get; init; }
    public bool SendEmailOnComplete { get; init; }

    public Budget ToEntity() => new()
    {
        Details = Details,
        Status = BudgetStatus.Pending,
        OrderNumber = OrderNumber,
        ExpectedCompletionDate = ExpectedCompletionDate,
        SendEmailOnComplete = SendEmailOnComplete,
        ProposedValue = ProposedValue
    };
}
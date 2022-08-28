using GODCommon.Enums;

namespace GODBudgets.Endpoints.Create;

public class CreateBudgetCommand
{
    public long OrderNumber { get; set; }
    public decimal ProposedValue { get; set; }
    public DateTime? ExpectedCompletionDate { get; set; }
    public string? Details { get; set; }
    public bool SendEmailOnComplete { get; set; }

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
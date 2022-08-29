using GODCommon.Entities;
using GODCommon.Enums;

namespace GODBudgets;

public class Budget : EntityBase.AsSnapshot
{
    public long OrderNumber { get; set; }
    public decimal? ProposedValue { get; set; }
    public BudgetStatus Status { get; set; }
    public DateTime? ExpectedCompletionDate { get; set; }
    public string? Details { get; set; }
    public bool SendEmailOnComplete { get; set; }
}
using GODCommon.Entities;
using GODCommon.Enums;

namespace GODBudgets;

public sealed class Budget : EntityBase.AsSnapshot
{
    public long OrderNumber { get; set; }
    public decimal? ProposedValue { get; set; }
    public BudgetStatus Status { get; set; }
    public int? WorkingDaysToComplete { get; set; }
    public string? Details { get; set; }
    public bool SendEmailOnComplete { get; set; }
}
using GODCommon.Enums;
using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODBudgets.Endpoints.Create;

public sealed class CreateBudgetCommand : IRequest<IResult<EventResult<Budget>>>
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
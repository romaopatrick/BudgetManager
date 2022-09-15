using GODCommon.Enums;
using GODCommon.Results;
using GODCommon.Results.Paging;
using MediatR;

namespace GODBudgets.Endpoints.Retrieve;

public sealed class RetrieveBudgetsCommand : Paged, IRequest<IResult<Paged<Budget>>>
{
    public long? OrderNumber { get; init; }
    public decimal? ProposedValueMin { get; init; }
    public decimal? ProposedValueMax { get; init; }
    public BudgetStatus? Status { get; init; }
    public int? WorkingDaysToCompleteMin { get; init; }
    public int? WorkingDaysToCompleteMax { get; init; }
}

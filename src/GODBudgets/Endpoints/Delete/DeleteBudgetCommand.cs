using GODCommon.Events;
using GODCommon.Results;
using MediatR;

namespace GODBudgets.Endpoints.Delete;

public sealed class DeleteBudgetCommand : IRequest<IResult<EventResult<Budget>>>
{
    public Guid BudgetId { get; init; }
}
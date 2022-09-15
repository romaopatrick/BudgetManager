using GODBudgets.Infra;
using GODCommon.Endpoints;
using GODCommon.Results;
using MediatR;

namespace GODBudgets.Endpoints;

public abstract class BaseHandler<TCommand, TResult> : BaseHandler<TCommand, TResult, DefaultContext>
    where TCommand : IRequest<IResult<TResult>>, new()
{
    public BaseHandler(DefaultContext context) : base(context)
    {
    }
}
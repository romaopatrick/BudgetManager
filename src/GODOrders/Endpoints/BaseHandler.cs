using GODCommon.Endpoints;
using GODCommon.Results;
using GODOrders.Infra;
using MediatR;

namespace GODOrders.Endpoints;

public abstract class BaseHandler<TCommand, TResult> : BaseHandler<TCommand, TResult, DefaultContext>
    where TCommand : IRequest<IResult<TResult>>, new()
{
    public BaseHandler(DefaultContext context) : base(context)
    {
    }
}



using GODCommon.Endpoints;
using GODCommon.Results;
using GODProducts.Infra;
using MediatR;

namespace GODProducts.Endpoints;

public abstract class BaseHandler<TCommand, TResult> : BaseHandler<TCommand, TResult, DefaultContext>
    where TCommand : IRequest<IResult<TResult>>, new()
{
    public BaseHandler(DefaultContext context) : base(context)
    {
    }
}

using GODCommon.Endpoints;
using GODCommon.Results;
using GODCustomers.Infra;
using MediatR;

namespace GODCustomers.Endpoints;

public abstract class BaseHandler<TCommand, TResult> : BaseHandler<TCommand, TResult, DefaultContext> 
    where TCommand : IRequest<IResult<TResult>>, new()
{
    public BaseHandler(DefaultContext context) : base(context)
    {
    }
}



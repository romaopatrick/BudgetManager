using GODCommon.Endpoints;
using GODOrders.Infra;

namespace GODOrders.Endpoints;

public abstract class BaseEndpoint<TCommand, TResult> : BaseEndpoint<TCommand, TResult, DefaultContext> where TCommand : notnull, new()
{
    public BaseEndpoint(DefaultContext context) : base(context)
    {
    }
}



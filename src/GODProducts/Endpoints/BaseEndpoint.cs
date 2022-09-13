using GODCommon.Endpoints;
using GODProducts.Infra;

namespace GODProducts.Endpoints;

public abstract class BaseEndpoint<TCommand, TResult> : BaseEndpoint<TCommand, TResult, DefaultContext> where TCommand : notnull, new()
{
    public BaseEndpoint(DefaultContext context) : base(context)
    {
    }
}

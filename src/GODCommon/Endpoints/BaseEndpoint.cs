using FastEndpoints;
using GODCommon.Results;

namespace GODCommon.Endpoints;

public abstract class BaseEndpoint<TCommand, TResult> : Endpoint<TCommand, IResult<TResult>> 
    where TCommand : notnull, new()
{
}
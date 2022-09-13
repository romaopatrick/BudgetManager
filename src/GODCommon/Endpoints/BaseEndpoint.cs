using FastEndpoints;
using GODCommon.Results;
using Microsoft.EntityFrameworkCore;

namespace GODCommon.Endpoints;

public abstract class BaseEndpoint<TCommand, TResult, TContext> : Endpoint<TCommand, IResult<TResult>> 
    where TCommand : notnull, new()
    where TContext : DbContext
{
    protected readonly TContext Context;
    protected BaseEndpoint(TContext context) => Context = context;
}
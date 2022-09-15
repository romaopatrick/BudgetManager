using GODCommon.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GODCommon.Endpoints;

public abstract class BaseHandler<TCommand, TResult, TContext> : IRequestHandler<TCommand, IResult<TResult>>
    where TCommand : IRequest<IResult<TResult>>, new()
        where TContext : DbContext
{
    protected readonly TContext Context;
    protected BaseHandler(TContext context) => Context = context;
    
    public abstract Task<IResult<TResult>> Handle(TCommand req, CancellationToken ct);
}

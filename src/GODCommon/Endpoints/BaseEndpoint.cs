using FastEndpoints;
using GODCommon.Results;
using MediatR;

namespace GODCommon.Endpoints;

public abstract class BaseEndpoint<TCommand, TResult> : Endpoint<TCommand, IResult<TResult>> 
    where TCommand : IRequest<IResult<TResult>>, new()
{
    protected readonly IMediator Mediator;
    protected BaseEndpoint(IMediator mediator) => Mediator = mediator;

    public override async Task HandleAsync(TCommand req, CancellationToken ct)
    {
        var result = await Mediator.Send(req, ct);
        if (result.Success) await SendAsync(result, (int)result.StatusCode, ct);
        else await SendAsync(result, (int)result.StatusCode, ct);
    }
}
using GODCommon.Results;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GODCommon.Consumers;

public abstract class BaseConsumer<TCorrelatedCommand, TResult> : IConsumer<TCorrelatedCommand>
    where TCorrelatedCommand : class, IRequest<IResult<TResult>>, ICorrelated<IRequest<IResult<TResult>>>
{
    protected readonly IMediator Mediator;
    protected readonly ILogger Logger;

    protected BaseConsumer(IMediator mediator, ILogger logger)
    {
        Mediator = mediator;
        Logger = logger;
    }

    public async Task Consume(ConsumeContext<TCorrelatedCommand> context)
    {
        var command = context.Message.UnCorrelate();
        var result = await Mediator.Send(command);

        if (result.Success)
            Logger.LogInformation(
                "[{Now}] - ID: [{CorrelationId}] \n\t - Message processed successfully with status {StatusCode} \n\t - Message: {Message} \n\t - Result: {Result}",
                DateTime.UtcNow, context.Message.CorrelationId, result.StatusCode, context.Message, result);

        else
            Logger.LogError(
                "[{Now}] - ID: [{CorrelationId}] \n\t - Message processed with failure with status {StatusCode} \n\t - Message: {Message} \n\t - Result: {Result}",
                DateTime.UtcNow, context.Message.CorrelationId, result.StatusCode, context.Message, result);
    }
}
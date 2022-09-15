using IResult = GODCommon.Results.IResult;

namespace GODConductor.Endpoints;

public abstract class PublishEndpoint<TCommand> : Endpoint<TCommand, IResult> where TCommand : notnull, new()
{
    
}
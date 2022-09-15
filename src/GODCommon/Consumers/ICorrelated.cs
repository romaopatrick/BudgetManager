using MassTransit;

namespace GODCommon.Consumers;

public interface ICorrelated<out TCommand> : CorrelatedBy<Guid>
{
    TCommand UnCorrelate();
}
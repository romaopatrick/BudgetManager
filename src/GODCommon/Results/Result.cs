using FluentValidation.Results;

namespace GODCommon.Results;

public interface IResult<out TResult> : IResult
{
    TResult Data { get; }
}
public interface IResult
{
    IEnumerable<string> Notifications { get; }
    bool Success { get; }
}
public class Result<TResult> : Result, IResult<TResult>
{
    internal Result(TResult data, bool success) : base(success)
    {
        Data = data;
        Success = success;
    }

    internal Result(string notification, bool success) : base(notification, success) {}

    internal Result(IEnumerable<string> notifications, bool success) : base(notifications, success)
    { }

    internal Result(ValidationResult result, bool success) : base(result, success)
    {}

    public TResult Data { get; } = default!;
}

public class Result : IResult
{
    internal Result(bool success) => Success = success;

    internal Result(string notification, bool success)
    {
        Notifications = new[] { notification };
        Success = success;
    }

    internal Result(IEnumerable<string> notifications, bool success)
    {
        Notifications = notifications;
        Success = success;
    }

    internal Result(ValidationResult result, bool success)
    {
        Notifications = from error in result.Errors select error.ErrorCode;
        Success = success;
    }

    public IEnumerable<string> Notifications { get; } = Enumerable.Empty<string>();
    public bool Success { get; protected init; }
}

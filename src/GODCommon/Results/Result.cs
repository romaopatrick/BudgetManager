using System.Net;
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
    HttpStatusCode StatusCode { get; }
}
public sealed class Result<TResult> : Result, IResult<TResult>
{
    internal Result(TResult data, bool success, HttpStatusCode statusCode) : base(success, statusCode)
    {
        Data = data;
        Success = success;
        StatusCode = statusCode;
    }

    internal Result(string notification, bool success, HttpStatusCode statusCode) : base(notification, success, statusCode) {}

    internal Result(IEnumerable<string> notifications, bool success, HttpStatusCode statusCode) : base(notifications, success, statusCode)
    { }

    internal Result(ValidationResult result, bool success, HttpStatusCode statusCode) : base(result, success, statusCode)
    {}

    public TResult Data { get; } = default!;
}

public class Result : IResult
{
    internal Result(bool success, HttpStatusCode statusCode) => Success = success;

    internal Result(string notification, bool success, HttpStatusCode statusCode)
    {
        Notifications = new[] { notification };
        Success = success;
        StatusCode = statusCode;
    }

    internal Result(IEnumerable<string> notifications, bool success, HttpStatusCode statusCode)
    {
        Notifications = notifications;
        Success = success;
        StatusCode = statusCode;
    }

    internal Result(ValidationResult result, bool success, HttpStatusCode statusCode)
    {
        Notifications = from error in result.Errors select error.ErrorCode;
        Success = success;
        StatusCode = statusCode;
    }

    public IEnumerable<string> Notifications { get; } = Enumerable.Empty<string>();
    public bool Success { get; protected init; }
    public HttpStatusCode StatusCode { get; protected init;  }
}

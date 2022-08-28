using System.Net;
using FluentValidation.Results;

namespace GODCommon.Results;

public interface IResult<TResult> : IResult
{
    public TResult Data { get; set; }
}
public interface IResult
{
    public IEnumerable<string> Notifications { get; set; }
    public int StatusCode { get; set; }
    public bool Success { get; set; }
}
public class Result<TResult> : Result, IResult<TResult>
{
    internal Result(TResult data, HttpStatusCode statusCode, bool success) : base(statusCode, success)
    {
        Data = data;
        StatusCode = (int)statusCode;
        Success = success;
    }

    internal Result(string notification, HttpStatusCode statusCode, bool success) : base(notification, statusCode, success)
    {}
    internal Result(IEnumerable<string> notifications, int statusCode, bool success) : base(notifications, statusCode, success)
    { }

    internal Result(ValidationResult result, HttpStatusCode statusCode, bool success) : base(result, statusCode, success)
    {}

    public TResult Data { get; set; }
}

public class Result : IResult
{
    internal Result(HttpStatusCode statusCode, bool success)
    {
        StatusCode = (int)statusCode;
        Success = success;
    }
    internal Result(string notification, HttpStatusCode statusCode, bool success)
    {
        Notifications = new[] { notification };
        StatusCode = (int)statusCode;
        Success = success;
    }

    internal Result(IEnumerable<string> notifications, int statusCode, bool success)
    {
        Notifications = notifications;
        StatusCode = statusCode;
        Success = success;
    }

    internal Result(ValidationResult result, HttpStatusCode statusCode, bool success)
    {
        Notifications = from error in result.Errors select error.ErrorCode;
        StatusCode = (int)statusCode;
        Success = success;
    }

    public IEnumerable<string> Notifications { get; set; }
    public bool Success { get; set; }
    public int StatusCode { get; set; }
}

using System.Net;
using FluentValidation.Results;

namespace GODCommon.Results;

public static class ResultFactory
{
    public static IResult<TResult> WithSuccess<TResult>(TResult result, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Result<TResult>(result, statusCode, true);

    public static IResult WithSuccess(HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Result(statusCode, true);

    public static IResult<TResult> WithError<TResult>(string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<TResult>(code, statusCode, false);

    public static IResult WithError(string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(code, statusCode, false);

    public static IResult<TResult> WithError<TResult>(ValidationResult validation, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<TResult>(validation, statusCode, false);
    public static IResult WithError(ValidationResult validation, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(validation, statusCode, false);

    public static IResult<TResult> WithError<TResult>(IEnumerable<string> notifications, int statusCode = (int)HttpStatusCode.BadRequest)
        => new Result<TResult>(notifications, statusCode, false);
    public static IResult WithError(IEnumerable<string> notifications, int statusCode = (int)HttpStatusCode.BadRequest)
        => new Result(notifications, statusCode, false);
}
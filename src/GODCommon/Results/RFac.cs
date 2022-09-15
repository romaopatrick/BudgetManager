using System.Net;
using FluentValidation.Results;

namespace GODCommon.Results;

public static class RFac
{
    public static IResult<TResult> WithSuccess<TResult>(TResult result, HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Result<TResult>(result, true, statusCode);

    public static IResult WithSuccess(HttpStatusCode statusCode = HttpStatusCode.OK)
        => new Result(true, statusCode);

    public static IResult<TResult> WithError<TResult>(string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<TResult>(code, false, statusCode);

    public static IResult WithError(string code, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(code, false, statusCode);

    public static IResult<TResult> WithError<TResult>(ValidationResult validation, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<TResult>(validation, false, statusCode);
    public static IResult WithError(ValidationResult validation, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(validation, false, statusCode);

    public static IResult<TResult> WithError<TResult>(IEnumerable<string> notifications, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result<TResult>(notifications, false, statusCode);
    public static IResult WithError(IEnumerable<string> notifications, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new Result(notifications, false, statusCode);
}
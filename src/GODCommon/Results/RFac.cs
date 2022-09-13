using FluentValidation.Results;

namespace GODCommon.Results;

public static class RFac
{
    public static IResult<TResult> WithSuccess<TResult>(TResult result)
        => new Result<TResult>(result, true);

    public static IResult WithSuccess()
        => new Result(true);

    public static IResult<TResult> WithError<TResult>(string code)
        => new Result<TResult>(code, false);

    public static IResult WithError(string code)
        => new Result(code, false);

    public static IResult<TResult> WithError<TResult>(ValidationResult validation)
        => new Result<TResult>(validation, false);
    public static IResult WithError(ValidationResult validation)
        => new Result(validation, false);

    public static IResult<TResult> WithError<TResult>(IEnumerable<string> notifications)
        => new Result<TResult>(notifications, false);
    public static IResult WithError(IEnumerable<string> notifications)
        => new Result(notifications, false);
}
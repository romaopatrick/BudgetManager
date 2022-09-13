using FastEndpoints;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using IResult = GODCommon.Results.IResult;

namespace GODCommon.Processors.Pre;

public class StandardizeResponseProcessor : IGlobalPostProcessor
{
    public Task PostProcessAsync(object req, object res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures, CancellationToken ct)
    {
    }
}
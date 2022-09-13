using FastEndpoints;
using FluentValidation.Results;
using GODCommon.Results;
using Microsoft.AspNetCore.Http;

namespace GODCommon.Processors.Pre;

public sealed class ValidationFailureProcessor : IGlobalPreProcessor
{
    public async Task PreProcessAsync(object req, HttpContext ctx, List<ValidationFailure> failures, CancellationToken ct)
    {
        if (failures.Count > 0)
            await ctx.Response.SendAsync(RFac.WithError(
                from failure in failures select failure.ErrorCode), cancellation: ct);
    }
}
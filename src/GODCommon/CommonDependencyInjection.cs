using System.Text.Json.Serialization;
using FastEndpoints;
using GODCommon.Processors.Pre;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GODCommon;

public static class DependencyInjection
{
    public static void UseCommonFastEndpoints(this WebApplication app, string appName) =>
        app.UseFastEndpoints(c =>
        {
            c.Endpoints.Configurator = ep =>
            {
                ep.Throttle(120, 60, "X-Client-Id");
                ep.DontThrowIfValidationFails();
                ep.DontAutoTag();
                ep.Description(d => d.WithTags(appName));
                ep.PreProcessors(new ValidationFailureProcessor());
            };
            c.Serializer.Options.PropertyNameCaseInsensitive = true;
            c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
            c.Endpoints.RoutePrefix = "api";
        });
}
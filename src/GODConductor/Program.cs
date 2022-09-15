global using FastEndpoints;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using GODCommon;
using GODCommon.Processors.Pre;
using GODConductor;
using Newtonsoft.Json.Converters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddHealthChecks();
builder.Services.AddSwaggerDoc(c =>
{
    c.Title = "GODConductor";
    c.Version = "v1";
    c.SerializerSettings.Converters.Add(new StringEnumConverter());
});
builder.Services.AddAuthentication();

builder.Services.AddLogging()
    .AddApplicationDependencies(builder.Configuration);

builder.Services.AddFastEndpoints();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();    
    app.UseSwaggerUi3(s => s.ConfigureDefaults());
}

app.UseHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
{
    c.Endpoints.Configurator = ep =>
    {
        ep.Throttle(120, 60, "X-Client-Id");
        ep.DontThrowIfValidationFails();
        ep.DontAutoTag();
        ep.Description(d => d.WithTags("conductor"));
        ep.PreProcessors(new ValidationFailureProcessor());
    };
    c.Serializer.Options.PropertyNameCaseInsensitive = true;
    c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
    c.Endpoints.RoutePrefix = "api/conductor";
});

app.Run();
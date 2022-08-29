global using FastEndpoints;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using GODBudgets;
using GODBudgets.Infra;
using GODCommon.Contexts;
using GODCommon.Processors.Pre;
using Newtonsoft.Json.Converters;
using NSwag;
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
    c.Title = "GODBudgets";
    c.Version = "v1";
    c.AddAuth("ApiKey", new()
    {
        Name = "budgets_api_key",
        In = OpenApiSecurityApiKeyLocation.Header,
        Type = OpenApiSecuritySchemeType.ApiKey
    });
    c.SerializerSettings.Converters.Add(new StringEnumConverter());
});
builder.Services.AddAuthentication();
builder.Services.AddLogging()
    .AddApplicationServices(builder.Configuration);
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
        ep.Description(d => d.WithTags("Budgets"));
        ep.PreProcessors(new ValidationFailureProcessor());
    };
    c.Serializer.Options.PropertyNameCaseInsensitive = true;
    c.Serializer.Options.Converters.Add(new JsonStringEnumConverter());
    c.Endpoints.RoutePrefix = "api";
});

app.UseAutoMigration<DefaultContext, Budget>();

app.Run();
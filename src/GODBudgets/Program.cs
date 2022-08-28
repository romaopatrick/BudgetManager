global using FastEndpoints;
using FastEndpoints.Swagger;
using GODBudgets;
using GODBudgets.Infra;
using GODCommon.Contexts;
using NSwag;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddFastEndpoints();
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
});
builder.Services.AddAuthentication();
builder.Services.AddLogging()
    .AddApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();    
    app.UseSwaggerUi3(s => s.ConfigureDefaults());
}

app.UseHealthChecks("/health");

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

app.UseAutoMigration<DefaultContext, Budget>();

app.Run();

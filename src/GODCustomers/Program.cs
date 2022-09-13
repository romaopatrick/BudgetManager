global using FastEndpoints;
using FastEndpoints.Swagger;
using GODCustomers;
using GODCustomers.Infra;
using GODCommon;
using GODCommon.Contexts;
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
    c.Title = "GODCustomers";
    c.Version = "v1";
    c.AddAuth("ApiKey", new()
    {
        Name = "customers_api_key",
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

app.UseCommonFastEndpoints("customers");
app.UseAutoMigration<DefaultContext, Customer>();

app.Run();
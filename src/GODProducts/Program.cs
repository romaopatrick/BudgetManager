global using FastEndpoints;
using FastEndpoints.Swagger;
using GODCommon;
using GODCommon.Contexts;
using GODProducts;
using GODProducts.Infra;
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
    c.Title = "GODOrders";
    c.Version = "v1";
    c.AddAuth("ApiKey", new()
    {
        Name = "orders_api_key",
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

app.UseCommonFastEndpoints("orders");

app.UseAutoMigration<DefaultContext>();

app.Run();
using GODCommon.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GODCommon.Contexts;

public static class ContextDependencyInjectionExtensions
{
    public static IServiceCollection AddContext<TContext>(this IServiceCollection services,
        IConfiguration configuration) where TContext : DbContext =>
        services
            .AddDbContext<TContext>(opts
                => opts.UseNpgsql(configuration.GetConnectionString(typeof(TContext).Name))
                    .UseSnakeCaseNamingConvention());

    public static void UseAutoMigration<TContext>(this IApplicationBuilder app) 
        where TContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<TContext>().Database.Migrate();
    }
}
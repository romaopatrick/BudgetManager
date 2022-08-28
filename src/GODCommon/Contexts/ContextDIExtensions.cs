using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GODCommon.Contexts;

public static class ContextDIExtensions
{
    public static IServiceCollection AddContext<TContext, TSnapshot>(this IServiceCollection services,
        IConfiguration configuration) where TContext : DefaultContextBase<TContext, TSnapshot> => services
        .AddDbContext<TContext>(opts
            => opts.UseNpgsql(configuration.GetConnectionString(typeof(TContext).Name))
                .UseSnakeCaseNamingConvention());
    
    public static void UseAutoMigration<TContext, TSnapshot>(this IApplicationBuilder app) where TContext : DefaultContextBase<TContext, TSnapshot>
    {
        using var scope = app.ApplicationServices.CreateScope();
        scope.ServiceProvider.GetRequiredService<TContext>().Database.Migrate();
    }
}
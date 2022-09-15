using MassTransit;

namespace GODConductor;

public static class DependencyInjection
{
    public static void AddApplicationDependencies(
        this IServiceCollection services,
        IConfiguration configuration)
        => services.ConfigureMassTransit(configuration);

    private static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(opts =>
        {
            opts.UsingRabbitMq((ctx, cfg) =>
            {
                var rabbitSection = configuration.GetSection("MessageContext");
                cfg.Host(rabbitSection.GetValue<string>("Host"), "/", h =>
                {
                    h.Username(rabbitSection.GetValue<string>("Username"));
                    h.Password(rabbitSection.GetValue<string>("Password"));
                });
                
                cfg.ConfigureEndpoints(ctx, new DefaultEndpointNameFormatter(false));
            });
        });
    }
}
using GODCommon.Contexts;
using GODOrders.Consumers.Create;
using GODOrders.Infra;
using MassTransit;
using MediatR;

namespace GODOrders;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext>(configuration)
            .AddMediatR(typeof(DependencyInjection))
            .ConfigureMassTransit(configuration);

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
            opts.AddConsumer<CreateOrderConsumer>();
        });
    }
}
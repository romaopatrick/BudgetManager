using GODCommon.Contexts;
using GODOrders.Infra;

namespace GODOrders;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext, Order>(configuration);
}
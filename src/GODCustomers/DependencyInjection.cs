using GODCommon.Contexts;
using GODCustomers.Infra;

namespace GODCustomers;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext, Customer>(configuration);
}
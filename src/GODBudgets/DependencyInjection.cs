using GODBudgets.Infra;
using GODCommon.Contexts;

namespace GODBudgets;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext, Budget>(configuration);
}
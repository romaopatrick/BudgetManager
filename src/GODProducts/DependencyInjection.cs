using GODCommon.Contexts;
using GODProducts.Infra;

namespace GODProducts;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext>(configuration);
}
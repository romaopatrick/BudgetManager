using GODBudgets.Infra;
using GODCommon.Contexts;
using MediatR;

namespace GODBudgets;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext>(configuration)
            .AddMediatR(typeof(DependencyInjection));
}
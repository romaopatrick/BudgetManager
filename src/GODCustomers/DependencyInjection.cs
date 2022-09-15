using GODCommon.Contexts;
using GODCustomers.Infra;
using MediatR;

namespace GODCustomers;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddContext<DefaultContext>(configuration)
            .AddMediatR(typeof(DependencyInjection));
}
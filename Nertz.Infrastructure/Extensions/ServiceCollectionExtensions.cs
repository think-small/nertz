using Microsoft.Extensions.DependencyInjection;
using Nertz.Infrastructure.Contracts;
using Nertz.Infrastructure.Repositories;

namespace Nertz.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<INertzRepository, NertzRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        return services;
    }
}
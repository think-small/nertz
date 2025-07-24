using Microsoft.Extensions.DependencyInjection;
using Nertz.Infrastructure.Contracts;
using Nertz.Infrastructure.Repositories;

namespace Nertz.Application.Extensions;

public static class InfrastructureCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<INertzRepository, NertzRepository>();
        return services;
    }
}
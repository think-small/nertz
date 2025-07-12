using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nertz.Application.Contracts;
using Nertz.Application.Factories;
using Nertz.Application.Nertz;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Application.Nertz.Shared.Utility;
using Nertz.Domain.ValueObjects;

namespace Nertz.Application.Extensions;

public static class ServiceCollectionExtensions  
{
    public static IServiceCollection AddNertzSetupOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<GameSetupOptions>(config.GetSection(GameSetupOptions.NertzSetup));
        return services;
    }

    public static IServiceCollection AddNertzGameMechanics(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IFactory<CardStack, CardStackType, Card>, CardStackFactory>();
        services.AddScoped<IShuffle, RandomShuffle>();

        services.AddScoped<INertz, NertzApplication>();
        
        return services;
    }
}
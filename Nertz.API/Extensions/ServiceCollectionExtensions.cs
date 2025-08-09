using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nertz.Application.Contracts;
using Nertz.Application.Shared.Factories;
using Nertz.Application.Nertz;
using Nertz.Application.Shared.Interfaces;
using Nertz.Application.Shared.Utility;
using Nertz.Domain.Cards;

namespace Nertz.Application.Extensions;

public static class ServiceCollectionExtensions  
{
    public static IServiceCollection AddNertzApi(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        services.SwaggerDocument();
        services.AddSignalR();
        return services;
    }
    
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
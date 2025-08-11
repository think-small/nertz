using FastEndpoints;
using FastEndpoints.Swagger;
using Nertz.API.Features.Games.Shared;
using Nertz.API.Shared.Factories;
using Nertz.API.Shared.Interfaces;
using Nertz.API.Shared.Utility;
using Nertz.Domain.Cards;

namespace Nertz.API.Extensions;

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
        return services;
    }
}
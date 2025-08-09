using FastEndpoints;
using FastEndpoints.Swagger;
using Nertz.Application.Extensions;
using Nertz.Application.Nertz.Features.Games;
using Nertz.Infrastructure.Extensions;

namespace Nertz.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddUserSecrets<Program>();

            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddNertzApi();
            builder.Services.AddNertzSetupOptions(builder.Configuration);
            builder.Services.AddNertzGameMechanics(builder.Configuration);
            builder.Services.AddInfrastructure();

            builder.Services.AddHttpClient("Nertz.API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7075/api");
            });
            builder.Services.AddScoped(s => s.GetRequiredService<IHttpClientFactory>().CreateClient("Nertz.API"));
            builder.Services.AddFastEndpoints();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseSwaggerGen();
            app.UseFastEndpoints();
            app.MapHub<GameHub>("/game");
            app.Run();
        }
    }
}

using FastEndpoints;
using FastEndpoints.Swagger;
using Nertz.Application.Extensions;
using Nertz.Infrastructure.Extensions;
using Nertz.UI.Components;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();
